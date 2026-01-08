using Facade.TenantManagement.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Facade.TenantManagement.Services.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFacadeTenantManagementServices(this IServiceCollection services)
    {
        services.AddScoped<ITenantManagementService, TenantManagementService>();
        services.AddScoped<ITenantMemberManagementService, TenantMemberManagementService>();
        
        return services;
    }
}