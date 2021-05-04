
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
    public class OperationCountrySupportedLanguageConfiguration : IEntityTypeConfiguration<OperationCountrySupportedLanguage>
    {
        public void Configure(EntityTypeBuilder<OperationCountrySupportedLanguage> builder)
        {
            builder.HasKey(x => new { x.OperationCountryId, x.SupportedLanaguageId });

            builder.HasOne(x => x.OperationCountry).WithMany(x => x.SupportedLanguages).HasForeignKey(x => x.OperationCountryId);

            builder.HasOne(x => x.SupportedLanguage).WithMany().HasForeignKey(x => x.SupportedLanaguageId);
        }
    }
}
