using ELibrary.Application;
using ELibrary.Infrastructure;
using Microsoft.EntityFrameworkCore;
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
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction, ServiceLifetime contextLifetime = ServiceLifetime.Transient)
        {
            services.AddDbContext<ApplicationDbContext>(optionsAction, contextLifetime);
            services.AddTransient<IDataContext, ApplicationDbContext>();

            return services;
        }
    }
}
