using Microsoft.Extensions.DependencyInjection;
using Tenant.Client.Contracts;
using Tenant.Client.Contracts.Resources;
using Tenant.Client.Resources;

namespace Tenant.Client.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTenantClient(this IServiceCollection services)
    {
        services.AddScoped<ITenantResource, TenantResource>();
        services.AddScoped<ITenantMemberResource, TenantMemberResource>();
        services.AddScoped<ITenantClient, TenantClient>();
        
        return services;
    }
}