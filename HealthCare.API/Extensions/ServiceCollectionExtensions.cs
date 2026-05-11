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
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
            services.AddScoped<IMedicineRepository, MedicineRepository>();
            services.AddScoped<IPrescriptionService, PrescriptionService>();
            services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
            services.AddScoped<IMedicalRecordService, MedicalRecordService>();
            services.AddScoped<IBillingRepository, BillingRepository>();
            services.AddScoped<IBillingService, BillingService>();
            services.AddScoped<IHospitalRepository, HospitalRepository>();
            services.AddScoped<IHospitalService, HospitalService>();
            services.AddScoped<ILabRepository, LabRepository>();
            services.AddScoped<ILabService, LabService>();
            // User service
            services.AddScoped<HealthCare.Application.Services.IUserService, HealthCare.Infrastructure.Services.UserService>();

            return services;
        }
    }
}
