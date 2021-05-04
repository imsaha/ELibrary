using ELibrary.Domain.Models;
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
    public class InitiateNewRentCommand : IRequest<Result<long>>
    {
        public long BookId { get; set; }

        public string TenantName { get; set; }
        public string TenantMobile { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Today;
        public DateTime ScheduledReturnDate { get; set; } = DateTime.Today.AddDays(5);


        public class Validator : AbstractValidator<InitiateNewRentCommand>
        {
            private readonly IDataContext _dataContext;

            public Validator(IDataContext dataContext)
            {
                RuleFor(x => x.StartDate)
                    .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Start date must today onwards.");

                RuleFor(x => x.ScheduledReturnDate)
                    .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("Return schedule can not be older then rent date.");

                var mobileNumberRegex = @"^[0\w](\+\d{1,3}[- ]?)?\d{9}$";
                RuleFor(x => x.TenantMobile)
                    .NotEmpty().WithMessage("Tenant mobile number is required.")
                    .Matches(mobileNumberRegex).WithMessage("Enter a valid mobile number.");

                RuleFor(x => x.TenantName)
                    .NotEmpty().WithMessage("Tenant name is required.");


                RuleFor(x => x.BookId)
                    .GreaterThan(0).WithMessage("Select a valid book.")
                    .MustAsync(CheckBookIsAvailableToRentAsync).WithMessage("Book is not available for rent.");
                _dataContext = dataContext;
            }

            private async Task<bool> CheckBookIsAvailableToRentAsync(long bookId, CancellationToken cancellationToken)
            {
                return !(await _dataContext.Rents.AnyAsync(x => x.BookId == bookId && x.ActualReturnDate == null));
            }
        }


        public class Handler : IRequestHandler<InitiateNewRentCommand, Result<long>>
        {
            private readonly IDataContext _dataContext;
            private readonly IAppContext _appContext;

            public Handler(IDataContext dataContext, IAppContext appContext)
            {
                _dataContext = dataContext;
                _appContext = appContext;
            }

            public async Task<Result<long>> Handle(InitiateNewRentCommand request, CancellationToken cancellationToken)
            {
                var tenant = (await _dataContext.Tenents.FirstOrDefaultAsync(x => x.MobileNumber == request.TenantMobile, cancellationToken)) ?? new Domain.Models.Tenant();

                tenant.Name = request.TenantName;
                tenant.MobileNumber = request.TenantMobile;

                var rent = new Rent
                {
                    Tenant = tenant,
                    TenantId = tenant.Id,
                    OperationCountryId = _appContext.OperationCountryId,
                    BookId = request.BookId,
                    StartDate = request.StartDate,
                    ScheduledReturnDate = request.ScheduledReturnDate,
                    ActualReturnDate = null
                };

                _dataContext.Rents.Add(rent);
                await _dataContext.SaveChangesAsync(cancellationToken);
                return Result.Ok(rent.Id);
            }
        }
    }
}
