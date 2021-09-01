using FluentValidation;
using FluentValidation.Results;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ELibrary.Application.Behaviors
{
    internal class RequestValidationPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly IServiceProvider _serviceProvider;

        public RequestValidationPreProcessor(IEnumerable<IValidator<TRequest>> validators, IServiceProvider serviceProvider)
        {
            _validators = validators;
            _serviceProvider = serviceProvider;
        }
        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            try
            {
               await  validateRequestAsync(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task validateRequestAsync(TRequest request)
        {
            var failures = new List<ValidationFailure>();
            var context = new ValidationContext<TRequest>(request);
            foreach (var validator in _validators)
            {
                var result = await validator.ValidateAsync(context);
                if (!result.IsValid)
                    failures.AddRange(result.Errors);
            }

            if (failures.Count != 0)
                throw new AppValidationException(failures);
        }
    }
}
