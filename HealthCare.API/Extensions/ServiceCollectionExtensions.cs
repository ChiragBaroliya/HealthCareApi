using HealthCare.Application.Repositories;
using HealthCare.Application.Services;
using HealthCare.Infrastructure.Data;
using HealthCare.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHealthCareServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Use SQL Server with the DefaultConnection from configuration
            services.AddDbContext<HealthCareDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IPatientRepository, HealthCare.Infrastructure.Repositories.PatientRepository>();
            services.AddScoped<IPatientService, HealthCare.Application.Services.PatientService>();
            // User service
            services.AddScoped<HealthCare.Application.Services.IUserService, HealthCare.Infrastructure.Services.UserService>();

            return services;
        }
    }
}
