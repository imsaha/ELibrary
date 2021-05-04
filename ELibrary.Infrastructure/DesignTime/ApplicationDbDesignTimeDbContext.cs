using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Infrastructure.DesignTime
{
    internal class ApplicationDbDesignTimeDbContext : DesignTimeDbContextFactoryBase<ApplicationDbContext>
    {
        public ApplicationDbDesignTimeDbContext() : base("DefaultConnection")
        {
        }

        protected override ApplicationDbContext CreateNewInstance(DbContextOptions<ApplicationDbContext> options)
        {
            return new ApplicationDbContext(options);
        }
    }
}
