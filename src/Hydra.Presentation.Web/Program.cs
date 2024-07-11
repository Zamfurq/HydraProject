using Hydra.DataAccess;
using Hydra.Presentation.Web.Configurations;
using Hydra.Presentation.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Hydra.Presentation.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            IServiceCollection services = builder.Services;
            Dependencies.ConfigureServices(builder.Configuration, services);
            services.AddScoped<LoginServices>();
            services.AddScoped<CandidateServices>();
            services.AddScoped<BootcampServices>();
            services.AddScoped<CourseServices>();

            services.AddBusinessServices();
            services.AddControllersWithViews();

            services.AddControllers(
                options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = false
                );

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Login}");

            app.Run();
        }
    }
}