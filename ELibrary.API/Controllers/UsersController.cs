using ELibrary.Application;
using ELibrary.Application.Users.Commands;
using ELibrary.Application.Users.Dto;
using ELibrary.Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ELibrary.API.Controllers
{
    public class UsersController : V1Controller
    {
        private readonly IDataProtector _authProtector;
        private readonly IConfiguration _configuration;

        public UsersController(IMediator mediator,
            IConfiguration configuration,
            IDataProtectionProvider dataProtectionProvider) : base(mediator)
        {
            _authProtector = dataProtectionProvider.CreateProtector("authentication");
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(nameof(Token))]
        public async Task<IActionResult> Token([FromBody] GetUserByIdAndPasswordQuery query)
        {
            var response = await _mediator.Send(query);
            if (response == null)
                return Unauthorized();
            var expiry = DateTime.UtcNow.AddDays(3);
            var token = generateToken(response, expiry, null);
            return Ok(Result.Ok(new {
                Token=token,
                Expiry= expiry
            }));
        }

        [HttpGet]
        [Route(nameof(SwitchRole))]
        public async Task<IActionResult> SwitchRole(string role)
        {
            var query = new GetUserByIdQuery(User.Identity.Name);
            var response = await _mediator.Send(query);
            if (response == null)
                return Unauthorized();

            var validRole = response.Roles.Contains(role);
            if(!validRole)
                return Unauthorized();

            var expiry = DateTime.UtcNow.AddDays(3);
            var token = generateToken(response, expiry, role);
            return Ok(Result.Ok(new
            {
                Token = token,
                Expiry = expiry
            }));
        }



        private string generateToken(UserDto user, DateTime expires, string actingRole)
        {
            var symmetricKey = Convert.FromBase64String(_configuration["APP_SECRET"]);
            var tokenHandler = new JwtSecurityTokenHandler();

            var identity = new ClaimsIdentity(new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(ClaimTypes.Name, user.Id),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim("actingRole", string.IsNullOrEmpty(actingRole)? user.ActingRole: actingRole),
            });

            foreach (var role in user.Roles)
                identity.AddClaim(new Claim(ClaimTypes.Role, role));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = expires,
                IssuedAt = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }
    }
}
