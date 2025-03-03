using CleanCQRS.Core.Interfaces;
using CleanCQRS.Core.IService;
using CleanCQRS.Core.Options;
using CleanCQRS.Core.ServiceInterface;
using CleanCQRS.Infrastructure.ApplicationDbContext;
using CleanCQRS.Infrastructure.Repository;
using CleanCQRS.Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CleanCQRS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection service)
        {
            //#OPTIONSPATTERN 4
            service.AddDbContext<AppDbContext>((provider, options) =>
            {
                options.UseSqlServer(provider.GetRequiredService<IOptionsSnapshot<ConnectionStringsOptions>>().Value.DefaultConnection);
            });


            service.AddScoped<IEmployeeRepository, EmployeeRepository>();
            service.AddScoped<IDoctorRepository, DoctorRepository>();
            service.AddScoped<IOTPVerifyService, OTPVerifyService>();
            service.AddScoped<IDoctorServiceRepository, DoctorServiceRepository>();
            service.AddScoped<IDoctorScheduleRepository, DoctorScheduleRepository>();
            service.AddScoped<IPatientRepository, PatientRepository>();
            service.AddScoped<IProvidedServicesRepository, ProvidedServicesRepository>();
            service.AddScoped<IAppointmentRepository, AppointmentRepository>();
            service.AddScoped<IAppointmentMediaRepository, AppointmentMediaRepository>();
            service.AddScoped<IAppointmentMediaUploadService, AppointmentMediaUploadService>();
            service.AddScoped<ExceptionLoggerService>();


            return service;
        }
    }
}
