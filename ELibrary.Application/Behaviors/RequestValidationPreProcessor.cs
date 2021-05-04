using FluentValidation;
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
        private readonly IServiceProvider _serviceProvider;

        public RequestValidationPreProcessor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            try
            {
                validateRequest(request);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Request", request);
                throw ex;
            }

            return Task.CompletedTask;
        }

        private void validateRequest(TRequest request)
        {
            var validators = typeof(TRequest).Assembly.GetTypes()
                .Where(x => (typeof(AbstractValidator<TRequest>)).IsAssignableFrom(x))
                .Select(s => (AbstractValidator<TRequest>)ActivatorUtilities.CreateInstance(_serviceProvider, s))
                .ToList();

            var context = new ValidationContext<TRequest>(request);

            var failures = validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null).ToList();

            if (failures.Count != 0)
                throw new AppValidationException(failures);
        }
    }
}
