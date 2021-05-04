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
    public class RentConfiguration : IEntityTypeConfiguration<Rent>
    {
        public void Configure(EntityTypeBuilder<Rent> builder)
        {
            builder.HasOne(x => x.Book).WithMany(x => x.Rents).HasForeignKey(x => x.BookId);
            builder.HasOne(x => x.Tenant).WithMany(x => x.Rents).HasForeignKey(x => x.TenantId);
            builder.HasOne(x => x.OperationCountry).WithMany().HasForeignKey(x => x.OperationCountryId);
        }
    }
}
