using Infrastructure.EventBus.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Project.Events.Contracts;
using Tenant.Events.Contracts;

namespace Board.Events.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBoardEvents(this IServiceCollection services)
    {
        services.AddScoped<IEventHandler<TenantCreatedEvent>, TenantEventsHandler>();
        services.AddScoped<IEventHandler<TenantDeletedEvent>, TenantEventsHandler>();
        services.AddScoped<IEventHandler<ProjectDeletedEvent>, ProjectEventsHandler>();
        
        return services;
    }
}