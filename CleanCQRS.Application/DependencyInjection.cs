using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;

namespace CleanCQRS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection service)
        {
            service.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                //#EventsLearn 0
                //this is for parallel execution.
                cfg.NotificationPublisher = new TaskWhenAllPublisher();
            });
            return service;
        }
    }
}
