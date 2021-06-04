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

namespace ELibrary.Application.BookManagement.Command
{
    public class BookRentByCountry
    {
        public long CountryId { get; set; }
        public double RentFee { get; set; }
    }

    public class SaveBookCommand : IRequest<Result<long>>
    {
        public long? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsProhibited { get; set; }
        public BookRentByCountry[] Fees { get; set; }

        public class Validator : AbstractValidator<SaveBookCommand>
        {
            private readonly IDataContext _dataContext;

            public Validator(IDataContext dataContext)
            {
                _dataContext = dataContext;


                RuleFor(x => x.Title)
                    .NotEmpty()
                    .Length(10, 50);

                RuleFor(x => x.Description)
                    .NotEmpty()
                    .Length(10, 255);

                When(x => x.Id > 0, () =>
                {
                    RuleFor(x => x.Id)
                    .MustAsync(BeValidBookIdAsync).WithMessage("Invalid book");
                });

                RuleForEach(x => x.Fees).MustAsync(BeAValidCountryAndValidFeeAsync).WithMessage("Invalid country value exists");
            }

            private async Task<bool> BeAValidCountryAndValidFeeAsync(BookRentByCountry fee, CancellationToken cancellationToken)
            {
                var isValidCountry = await _dataContext.OperationCountries.AnyAsync(x => x.Id == fee.CountryId, cancellationToken);
                var isFeeValid = fee.RentFee > 0;

                return isValidCountry && isFeeValid;
            }

            private async Task<bool> BeValidBookIdAsync(long? id, CancellationToken cancellationToken)
            {
                return await _dataContext.Books.AnyAsync(x => x.Id == id, cancellationToken);
            }
        }


        public class Handler : IRequestHandler<SaveBookCommand, Result<long>>
        {
            private readonly IDataContext _dataContext;

            public Handler(IDataContext dataContext)
            {
                _dataContext = dataContext;
            }
            public async Task<Result<long>> Handle(SaveBookCommand request, CancellationToken cancellationToken)
            {
                Book book = null;
                if (request.Id > 0)
                {
                    book = await _dataContext.Books
                        .Include(x => x.Fees)
                        .FirstOrDefaultAsync(x => x.Id == request.Id.Value, cancellationToken);
                    _dataContext.Books.Update(book);
                }
                else
                {
                    book = new Book();
                    _dataContext.Books.Add(book);
                }

                book.Title = request.Title;
                book.Description = request.Description;
                book.IsProhibited = request.IsProhibited;

                foreach (var item in book.Fees)
                    book.Fees.Remove(item);

                foreach (var fee in request.Fees)
                {
                    book.Fees.Add(new CountryBookRentFee()
                    {
                        BookId = book.Id,
                        OperationCountryId = fee.CountryId,
                        RentFee = fee.RentFee,
                    });
                }

                await _dataContext.SaveChangesAsync(cancellationToken);
                return Result.Ok(book.Id);

            }
        }
    }

}
