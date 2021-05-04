using ELibrary.Application.RentManagement.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ELibrary.Application.RentManagement.Queries
{
    public class GetActiveRentsQuery : IRequest<ListResult<ActiveRentDto>>
    {

        public class Handler : IRequestHandler<GetActiveRentsQuery, ListResult<ActiveRentDto>>
        {
            private readonly IDataContext _dataContext;
            private readonly IAppContext _appContext;

            public Handler(IDataContext dataContext, IAppContext appContext)
            {
                _dataContext = dataContext;
                _appContext = appContext;
            }
            public async Task<ListResult<ActiveRentDto>> Handle(GetActiveRentsQuery request, CancellationToken cancellationToken)
            {
                var query = _dataContext.Rents.Where(x => x.ActualReturnDate == null);

                var projection = query.Select(s => new ActiveRentDto()
                {
                    Id = s.Id,
                    BookTitle = s.Book.Title,
                    TenantName = s.Tenant.Name,
                    TenantMobile = s.Tenant.MobileNumber,
                    StartDate = s.StartDate,
                    ScheduledReturnDate = s.ScheduledReturnDate,
                }).OrderByDescending(x => x.StartDate);

                var data = await projection.ToListAsync(cancellationToken);
                return ListResult.Ok(data);
            }
        }
    }
}
