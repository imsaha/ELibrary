using ELibrary.Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ELibrary.Application.Users.Commands
{
    public class CreateUserCommand : IRequest
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
}
