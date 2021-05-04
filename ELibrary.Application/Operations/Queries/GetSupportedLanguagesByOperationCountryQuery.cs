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
    public class GetSupportedLanguagesByOperationCountryQuery : IRequest<ListResult<SupportedLanguageDto>>
    {
        public class Handler : IRequestHandler<GetSupportedLanguagesByOperationCountryQuery, ListResult<SupportedLanguageDto>>
        {
            private readonly IDataContext _dataContext;
            private readonly IAppContext _appContext;

            public Handler(IDataContext dataContext, IAppContext appContext)
            {
                _dataContext = dataContext;
                _appContext = appContext;
            }
            public async Task<ListResult<SupportedLanguageDto>> Handle(GetSupportedLanguagesByOperationCountryQuery request, CancellationToken cancellationToken)
            {

                var query = _dataContext.OperationCountries.Where(x => x.Id == _appContext.OperationCountryId);

                var projection = query.SelectMany(s => s.SupportedLanguages)
                    .Select(s => new SupportedLanguageDto()
                    {
                        Id = s.SupportedLanguage.Id,
                        Code = s.SupportedLanguage.Code,
                        Name = s.SupportedLanguage.Name
                    }).Distinct();

                var data = await projection.ToListAsync(cancellationToken);
                return ListResult.Ok(data);
            }
        }
    }
}
