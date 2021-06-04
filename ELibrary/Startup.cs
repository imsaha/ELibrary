using Askmethat.Aspnet.JsonLocalizer.Extensions;
using Askmethat.Aspnet.JsonLocalizer.JsonOptions;
using ELibrary.Application;
using ELibrary.Infrastructure;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
            services.AddApplication();
            services.AddInfrastructure(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            configureLocalization(services);

            services
               .AddIdentity<IdentityUser<long>, IdentityRole<long>>(options =>
               {
                   options.SignIn.RequireConfirmedAccount = true;
                   options.Password.RequireDigit = false;
                   options.Password.RequireLowercase = false;
                   options.Password.RequireNonAlphanumeric = false;
                   options.Password.RequireUppercase = false;
                   options.Password.RequiredLength = 5;

                   options.Lockout.MaxFailedAccessAttempts = 30;
               })
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Users", policy =>
                {
                    policy
                    .RequireAuthenticatedUser()
                    .RequireRole("user", "admin"); ;
                });

                options.AddPolicy("Admins", policy =>
                {
                    policy
                    .RequireAuthenticatedUser()
                    .RequireRole("admin");
                });
            });

            services.AddLogging(options =>
            {
                options.AddConsole();
                options.AddEventLog();
                options.AddDebug();
            });

            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizePage("/Index", "Users");
                options.Conventions.AuthorizeFolder("/Rents", "Admins");
            })
            .AddFluentValidation(fv =>
            {
                fv.LocalizationEnabled = true;
                fv.RegisterValidatorsFromAssemblies(new[] {
                    typeof(Startup).Assembly,
                    typeof(Result).Assembly});
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.Name = "tkn";

                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(1);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            initializeApplicationAsync(app).Wait();
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


        private async Task initializeApplicationAsync(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var logger = scope.ServiceProvider.GetService<ILogger<ApplicationDbContext>>();
                var appIdentityDbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
                await appIdentityDbContext.Database.MigrateAsync();

                var userManager = scope.ServiceProvider.GetService<UserManager<IdentityUser<long>>>();
                await createUser(logger, userManager,
                   userName: "sirajmsaha",
                   password: "P@ssw0rd2021",
                   email: "sirajmsaha@gmail.com",
                   role: "admin");

                await createUser(logger, userManager,
                  userName: "testuser1",
                  password: "P@ssw0rd2021",
                  email: "testuser@test.com",
                  role: "user");
            }
        }

        private static async Task createUser(ILogger<ApplicationDbContext> logger, UserManager<IdentityUser<long>> userManager, string userName, string password, string email, string role)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                user = new IdentityUser<long>(userName)
                {
                    Email = email,
                    EmailConfirmed = true,
                    UserName = userName
                };

                var result = await userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    logger.LogInformation($"User {user.UserName} creation failed.");
                    user = null;
                }
            }

            if (user != null)
            {
                // add to ADMIN role of not exists in
                if (!await (userManager.IsInRoleAsync(user, role)))
                    await userManager.AddToRoleAsync(user, role);

                logger.LogInformation($"({user.UserName}) successfully created.");
            }
        }
    }
}
