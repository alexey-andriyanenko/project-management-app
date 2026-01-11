using Infrastructure.EventBus.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EventBus.InMemory.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInMemoryEventBus(this IServiceCollection services)
    {
        services.AddScoped<IEventBus, InMemoryEventBus>();
        return services;
    }
}