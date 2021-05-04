using ELibrary.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Infrastructure.Configurations
{
    public class CountryBookRentFeeConfiguration : IEntityTypeConfiguration<CountryBookRentFee>
    {
        public void Configure(EntityTypeBuilder<CountryBookRentFee> builder)
        {
            builder.HasKey(x => new { x.OperationCountryId, x.BookId });

            builder.HasOne(x => x.Book).WithMany(x => x.Fees).HasForeignKey(x => x.BookId);
            builder.HasOne(x => x.OperationCountry).WithMany().HasForeignKey(x => x.OperationCountryId);
        }
    }
}
