using CleanCQRS.Application;
using CleanCQRS.Core;
using CleanCQRS.Infrastructure;

namespace CleanCQRS.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiDI(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddInfrastructureDI()
                    .AddApplicationDI()
                    //#OPTIONSPATTERN 5
                    .AddCoreDI(configuration);
            return service;
        }
    }
}
