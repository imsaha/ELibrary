using ELibrary.Application.Users.Dto;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ELibrary.Application.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public GetUserByIdQuery(string userId)
        {
            UserId = userId;
        }
        public string UserId { get; set; }

        public class Validator : AbstractValidator<GetUserByIdQuery>
        {
            public Validator()
            {
                RuleFor(x => x.UserId).NotEmpty().WithMessage("User id can not be empty!");
            }
        }

        public class Handler : IRequestHandler<GetUserByIdQuery, UserDto>
        {
            private readonly IDataContext _dataContext;
            private readonly IDataProtectionService _protectionService;

            public Handler(IDataContext dataContext, IDataProtectionService protectionService)
            {
                _dataContext = dataContext;
                _protectionService = protectionService;
            }
            public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
                if (user == null)
                    return null;

                var projection = _dataContext.Users.Where(x => x.Id == user.Id)
                    .Select(s => new UserDto
                    {
                        Id = s.Id,
                        Roles = s.Roles.Select(r => r.RoleId).ToArray(),
                        ActingRole = s.Roles.Select(s => s.RoleId).First()
                    });

                return await projection.FirstAsync(cancellationToken);
            }
        }
    }
}
