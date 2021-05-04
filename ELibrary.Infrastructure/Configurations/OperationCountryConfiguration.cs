using ELibrary.Domain.Models;
using ELibrary.Infrastructure.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Infrastructure.Configurations
{
    public class OperationCountryConfiguration : IEntityTypeConfiguration<OperationCountry>
    {
        public void Configure(EntityTypeBuilder<OperationCountry> builder)
        {
            builder.Property(x => x.WeekDays)
                .HasConversion(new EnumArrayToStringConverter<DayOfWeek>());
        }
    }
}
