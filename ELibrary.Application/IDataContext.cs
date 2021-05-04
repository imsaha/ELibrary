using ELibrary.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ELibrary.Application
{

    public interface IDataContext
    {
        public DbSet<Rent> Rents { get; }

        public DbSet<Tenant> Tenents { get; }

        public DbSet<Book> Books { get; }

        public DbSet<OperationCountry> OperationCountries { get; }

        public DbSet<CountryBookRentFee> CountryWiseBookRentFees { get; }
        DbSet<SupportedLanguage> SupportedLanguages { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
