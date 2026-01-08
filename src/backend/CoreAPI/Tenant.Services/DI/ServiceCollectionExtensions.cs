using Microsoft.Extensions.DependencyInjection;
using Tenant.Contracts.Services;

namespace Tenant.Services.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTenantServices(this IServiceCollection services)
    {
        services.AddScoped<ITenantService, TenantService>();
        services.AddScoped<ITenantMemberService, TenantMemberService>();
        
        return services;
    }
}