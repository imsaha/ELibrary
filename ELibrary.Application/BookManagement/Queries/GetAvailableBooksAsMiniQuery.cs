using ELibrary.Application.BookManagement.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ELibrary.Application.BookManagement.Queries
{
    public class GetAvailableBooksAsMiniQuery : IRequest<ListResult<BookMiniDto>>
    {

        public class Handler : IRequestHandler<GetAvailableBooksAsMiniQuery, ListResult<BookMiniDto>>
        {
            private readonly IDataContext _dataContext;
            private readonly IAppContext _appContext;

            public Handler(IDataContext dataContext, IAppContext appContext)
            {
                _dataContext = dataContext;
                _appContext = appContext;
            }
            public async Task<ListResult<BookMiniDto>> Handle(GetAvailableBooksAsMiniQuery request, CancellationToken cancellationToken)
            {
                var query = _dataContext.CountryWiseBookRentFees
                     .Include(x => x.OperationCountry)
                     .Include(x => x.Book)
                     .Where(x => x.OperationCountryId == _appContext.OperationCountryId && !x.Book.Rents.Any(x => x.ActualReturnDate == null));

                var projection = query.Select(s => new BookMiniDto()
                {
                    Id = s.Book.Id,
                    Title = s.Book.Title,
                    Fees = s.RentFee,
                    Currency = s.OperationCountry.Currency
                });

                var data = await projection.ToListAsync(cancellationToken);
                return ListResult.Ok(data);
            }
        }
    }
}
