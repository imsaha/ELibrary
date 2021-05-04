using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ELibrary.Application.RentManagement.Commands
{
    public class DeleteRentByIdCommand : IRequest<Result>
    {
        public DeleteRentByIdCommand(long rentId)
        {
            RentId = rentId;
        }

        public long RentId { get; set; }

        public class Validator : AbstractValidator<DeleteRentByIdCommand>
        {
            public Validator()
            {
                RuleFor(x => x.RentId)
                    .GreaterThan(0).WithMessage("Select a valid rent.");
            }
        }

        public class Handler : IRequestHandler<DeleteRentByIdCommand, Result>
        {
            private readonly IDataContext _dataContext;

            public Handler(IDataContext dataContext)
            {
                _dataContext = dataContext;
            }
            public async Task<Result> Handle(DeleteRentByIdCommand request, CancellationToken cancellationToken)
            {
                var activeRent = await _dataContext.Rents.FirstOrDefaultAsync(x => x.Id == request.RentId && x.ActualReturnDate == null, cancellationToken);

                if (activeRent == null)
                    throw new ArgumentException("The rent is already deleted or closed.");


                _dataContext.Rents.Remove(activeRent);
                await _dataContext.SaveChangesAsync(cancellationToken);

                return Result.Ok();
            }
        }
    }
}
