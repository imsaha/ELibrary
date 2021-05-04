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
    public class SupportedLanguageConfiguration : IEntityTypeConfiguration<SupportedLanguage>
    {
        public void Configure(EntityTypeBuilder<SupportedLanguage> builder)
        {
            builder.HasIndex(x => x.Code).IsUnique();
        }
    }
}
