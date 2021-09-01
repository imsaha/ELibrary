using ELibrary.Application.Users.Dto;
using ELibrary.Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ELibrary.Application.Users.Commands
{

    public class CreateUserCommand: IRequest
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string[] Roles { get; set; }


        public class Validator : AbstractValidator<CreateUserCommand>
        {
            public Validator()
            {
                RuleFor(x => x.UserId).NotEmpty().WithMessage("User id can not be empty!");
                RuleFor(x => x.Password).NotEmpty().WithMessage("Password can not be empty!");
            }
        }

        public class Handler : IRequestHandler<CreateUserCommand>
        {
            private readonly IDataContext _dataContext;
            private readonly IDataProtectionService _protectionService;

            public Handler(IDataContext dataContext, IDataProtectionService protectionService)
            {
                _dataContext = dataContext;
                _protectionService = protectionService;
            }
            public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var query = _dataContext.Users.Where(x => x.Id == request.UserId);
                if (await query.AnyAsync(cancellationToken))
                    throw new DuplicateNameException("User Id alreasy exists");



                var user = new User
                {
                    Id = request.UserId,
                    SecurityKey= Guid.NewGuid().ToString("N"),
                };

                user.PasswordHash = _protectionService.Protect($"{user.SecurityKey}{request.Password}");

                if (request.Roles.Length > 0)
                {
                    foreach (var role in request.Roles)
                    {
                        var roleExists = await _dataContext.Roles.AnyAsync(x => x.Id == role);
                        if (roleExists)
                            user.Roles.Add(new UserRole(role));
                    }
                }

                _dataContext.Users.Add(user);
                await _dataContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
    public class GetUserByIdAndPasswordQuery : IRequest<UserDto>
    {
        public string UserId { get; set; }
        public string Password{ get; set; }

        public class Validator : AbstractValidator<GetUserByIdAndPasswordQuery>
        {
            public Validator()
            {
                RuleFor(x => x.UserId).NotEmpty().WithMessage("User id can not be empty!");
                RuleFor(x => x.Password).NotEmpty().WithMessage("Password can not be empty!");
            }
        }

        public class Handler : IRequestHandler<GetUserByIdAndPasswordQuery, UserDto>
        {
            private readonly IDataContext _dataContext;
            private readonly IDataProtectionService _protectionService;

            public Handler(IDataContext dataContext, IDataProtectionService protectionService)
            {
                _dataContext = dataContext;
                _protectionService = protectionService;
            }
            public async Task<UserDto> Handle(GetUserByIdAndPasswordQuery request, CancellationToken cancellationToken)
            {
                var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId , cancellationToken);
                if (user == null)
                    return null;

                var unProtectedPassword = _protectionService.UnProtect(user.PasswordHash);
                if (unProtectedPassword != $"{user.SecurityKey}{request.Password}")
                    return null;

                var projection = _dataContext.Users.Where(x=>x.Id==user.Id)
                    .Select(s => new UserDto
                    {
                        Id = s.Id,
                        Roles = s.Roles.Select(r => r.RoleId).ToArray(),
                    });

                return await projection.FirstAsync(cancellationToken);
            }
        }
    }
}
