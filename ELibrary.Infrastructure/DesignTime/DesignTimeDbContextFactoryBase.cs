using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Infrastructure.DesignTime
{
    internal abstract class DesignTimeDbContextFactoryBase<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        private const string ASPNETCORE_ENVIRONMENT = "ASPNETCORE_ENVIRONMENT";
        private readonly string _connectionName;
        private readonly string _migrationAssembly;

        protected DesignTimeDbContextFactoryBase(string connectionName, string migrationAssembly = null)
        {
            _connectionName = connectionName;
            _migrationAssembly = migrationAssembly;
        }

        public TContext CreateDbContext(string[] args)
        {
            return Create(_connectionName, _migrationAssembly);
        }

        private TContext Create(string connectionName, string migrationAssembly)
        {
            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .AddJsonFile($"appsettings.Local.json", optional: true)
               .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable(ASPNETCORE_ENVIRONMENT)}.json", optional: true)
               .AddEnvironmentVariables()
               .Build();

            var connectionString = configuration.GetConnectionString(connectionName);

            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException($"Connection string '{_connectionName}' is null or empty.", nameof(connectionString));

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            migrationAssembly = string.IsNullOrEmpty(migrationAssembly) ? GetType().Assembly.GetName().Name : migrationAssembly;

            optionsBuilder.UseSqlServer(connectionString, config =>
            {
                config.MigrationsAssembly(migrationAssembly);
            });

            Console.WriteLine($"DesignTimeDbContextFactoryBase.Create(string): Connection string= '{connectionString}'.");
            return CreateNewInstance(optionsBuilder.Options);
        }

        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);
    }
}
