using ELibrary.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.API.Helpers.HttpFilters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(IWebHostEnvironment environment, ILogger<GlobalExceptionFilter> logger)
        {
            _environment = environment;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            _logger.LogError(exception, exception.Message);
            context.HttpContext.Response.ContentType = "application/json";

            var statusCode = mapExceptionToHttpStatusCode(exception);
            context.HttpContext.Response.StatusCode = (int)statusCode;
            context.Result = new JsonResult(Result.Failed(new
            {
                Status = statusCode,
                Message = exception.Message,
                Failures = exception is AppValidationException validationException ? validationException.Failures : null
            }));
            return;
        }

        private HttpStatusCode mapExceptionToHttpStatusCode(Exception exception)
        {
            if (exception is ArgumentNullException or ValidationException or ArgumentException)
                return HttpStatusCode.BadRequest;

            if (exception is NotImplementedException)
                return HttpStatusCode.NotImplemented;

            if (exception is UnauthorizedAccessException)
                return HttpStatusCode.Unauthorized;

            return HttpStatusCode.InternalServerError;
        }
    }
}
