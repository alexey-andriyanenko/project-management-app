using Microsoft.Extensions.DependencyInjection;

namespace Project.Events.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProjectEvents(this IServiceCollection services)
    {
        services.AddScoped<TenantEventsHandler>();
        return services;
    }
}