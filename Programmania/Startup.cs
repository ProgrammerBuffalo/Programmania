using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Programmania.Middlewares;
using Programmania.Models;
using Programmania.Services;
using Programmania.Services.Interfaces;
using System;

namespace Programmania
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
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(5);
            });

            services.AddCors();
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.IgnoreNullValues = true);

            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DAL.ProgrammaniaDBContext>(options => options.UseSqlServer(connection));

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IXMLService, XMLService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IPerformanceService, PerformanceService>();
            services.AddScoped<IStaticService, StaticService>();
            services.AddScoped<IAdminService, AdminService>();

            services.AddControllersWithViews()
                .AddViewLocalization();

            //services.AddLocalization(options => options.ResourcesPath = "Resources");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //var supportedCultures =  new[] {
            //    new System.Globalization.CultureInfo("en"),
            //    new System.Globalization.CultureInfo("ru")
            //};

            //app.UseRequestLocalization(new RequestLocalizationOptions() {
            //    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en"),
            //    SupportedCultures = supportedCultures,
            //    SupportedUICultures = supportedCultures
            //});

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseCors(x => x
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseMiddleware<ErrorMiddleware>();
            app.UseMiddleware<JWTMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
