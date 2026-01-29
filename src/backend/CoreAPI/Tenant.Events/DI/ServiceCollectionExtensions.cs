using Identity.Events.Contracts;
using Infrastructure.EventBus.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Tenant.Events.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTenantEvents(this IServiceCollection services)
    {
        services.AddScoped<IEventHandler<UserInvitationAcceptedEvent>, UserInvitationEventsHandler>();
        
        return services;
    }
}