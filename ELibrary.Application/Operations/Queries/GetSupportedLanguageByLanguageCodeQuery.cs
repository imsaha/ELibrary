using ELibrary.Application.Operations.Dtos;
using FluentValidation;
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
    public class GetSupportedLanguageByLanguageCodeQuery : IRequest<Result<SupportedLanguageDto>>
    {

        public GetSupportedLanguageByLanguageCodeQuery(string code)
        {
            Code = code;
        }
        public string Code { get; set; }

        public class Validator : AbstractValidator<GetSupportedLanguageByLanguageCodeQuery>
        {
            public Validator()
            {
                RuleFor(x => x.Code)
                    .NotEmpty().WithMessage("A valid language code is required.");
            }
        }

        public class Handler : IRequestHandler<GetSupportedLanguageByLanguageCodeQuery, Result<SupportedLanguageDto>>
        {
            private readonly IDataContext _dataContext;

            public Handler(IDataContext dataContext)
            {
                _dataContext = dataContext;
            }
            public async Task<Result<SupportedLanguageDto>> Handle(GetSupportedLanguageByLanguageCodeQuery request, CancellationToken cancellationToken)
            {
                var supportedLang = await _dataContext.SupportedLanguages.Where(x => x.Code == request.Code)
                    .Select(s => new SupportedLanguageDto()
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Code = s.Code
                    }).FirstOrDefaultAsync(cancellationToken);

                return Result.Ok(supportedLang);
            }
        }
    }
}
