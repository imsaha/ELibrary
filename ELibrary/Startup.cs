using Askmethat.Aspnet.JsonLocalizer.Extensions;
using Askmethat.Aspnet.JsonLocalizer.JsonOptions;
using ELibrary.Application;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELibrary
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<IAppContext, AppContext>();

            services.AddRazorPages()
                .AddFluentValidation(fv =>
                {
                    fv.LocalizationEnabled = true;
                    fv.RegisterValidatorsFromAssemblies(new[] {
                        typeof(Startup).Assembly,
                        typeof(Result).Assembly});
                });

            services.AddApplication();
            services.AddInfrastructure(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            configureLocalization(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            buildLocalization(app);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }

        private void configureLocalization(IServiceCollection services)
        {
            var jsonLocalizationOptions = Configuration.GetSection("LocalizationOptions").Get<JsonLocalizationOptions>();
            var defaultRequestCulture = new RequestCulture(jsonLocalizationOptions.DefaultCulture, jsonLocalizationOptions.DefaultUICulture);
            var supportedCultures = jsonLocalizationOptions.SupportedCultureInfos.ToList();
            services.AddJsonLocalization(options =>
            {
                options.ResourcesPath = jsonLocalizationOptions.ResourcesPath;
                options.UseBaseName = jsonLocalizationOptions.UseBaseName;
                options.CacheDuration = jsonLocalizationOptions.CacheDuration;
                options.SupportedCultureInfos = jsonLocalizationOptions.SupportedCultureInfos;
                options.FileEncoding = jsonLocalizationOptions.FileEncoding;
                options.IsAbsolutePath = jsonLocalizationOptions.IsAbsolutePath;
                options.LocalizationMode = LocalizationMode.I18n;
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = defaultRequestCulture;
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        }

        private static void buildLocalization(IApplicationBuilder app)
        {
            var localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value;
            app.UseRequestLocalization(localizationOptions);
        }
    }
}
