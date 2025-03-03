using CleanCQRS.Core.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanCQRS.Core
{
    //#OPTIONSPATTERN 3
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreDI(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<ConnectionStringsOptions>(configuration.GetSection(ConnectionStringsOptions.SectionName));
            return service;
        }
    }
}
