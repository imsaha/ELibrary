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
    public class GetBooksQuery : IRequest<ListResult<BookDto>>
    {
        public string Search { get; set; }

        public class Handler : IRequestHandler<GetBooksQuery, ListResult<BookDto>>
        {
            private readonly IDataContext _dataContext;
            private readonly IAppContext _appContext;

            public Handler(IDataContext dataContext, IAppContext appContext)
            {
                _dataContext = dataContext;
                _appContext = appContext;
            }
            public async Task<ListResult<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
            {
                var operationCountryId = _appContext.OperationCountryId;
                var query = _dataContext.Books
                    .Select(s => new BookDto()
                    {
                        Id = s.Id,
                        Title = s.Title,
                        Description = s.Description,
                        IsProhibited = s.IsProhibited,
                        RentFee = s.Fees.First(x => x.OperationCountryId == operationCountryId).RentFee
                    });

                var result = await query.ToListAsync(cancellationToken);
                return ListResult.Ok(result);
            }
        }
    }
}
