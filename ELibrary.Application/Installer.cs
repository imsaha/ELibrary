using ELibrary.Application;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary
{
    public static class Installer
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddMediatR(typeof(Result).Assembly);

            return services;
        }

        public static IServiceCollection AddApplication<TAppContext>(this IServiceCollection services) where TAppContext : IAppContext
        {
            services.AddApplication();
            services.AddTransient(typeof(IAppContext), typeof(TAppContext));
            return services;
        }
    }
}
