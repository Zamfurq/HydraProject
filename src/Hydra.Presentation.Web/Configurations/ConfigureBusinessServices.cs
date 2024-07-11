using Hydra.Business.Interfaces;
using Hydra.Business.Repositories;

namespace Hydra.Presentation.Web.Configurations
{
    public static class ConfigureBusinessServices
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {

            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped<IBootcampRepository, BootcampRepository>(); 
            services.AddScoped<ICourseRepository, CourseRepository>();
            return services;
        }
    }
}
