using ELibrary.Application.Operations.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ELibrary.Application.Operations.Queries
{
    public class GetOperationCountriesQuery : IRequest<ListResult<OperationCountryDto>>
    {
        public class Handler : IRequestHandler<GetOperationCountriesQuery, ListResult<OperationCountryDto>>
        {
            private readonly IDataContext _dataContext;

            public Handler(IDataContext dataContext)
            {
                _dataContext = dataContext;
            }

            public async Task<ListResult<OperationCountryDto>> Handle(GetOperationCountriesQuery request, CancellationToken cancellationToken)
            {
                var query = _dataContext.OperationCountries.AsQueryable();

                var projection = query.Select(s => new OperationCountryDto()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Alias = s.Alias,
                    Currency = s.Currency,
                });

                var data = await projection.ToListAsync(cancellationToken);
                return ListResult.Ok(data);
            }
        }
    }
}
