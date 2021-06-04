using System;
using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(ELibrary.Areas.Identity.IdentityHostingStartup))]
namespace ELibrary.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}
