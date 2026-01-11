using Facade.TenantManagement.Services.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tenant.DI;

namespace Facade.TenantManagement.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFacadeTenantManagementModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTenantModule(configuration);
        services.AddFacadeTenantManagementServices();
        
        return services;
    }

    public static IMvcBuilder AddFacadeTenantManagementControllers(this IMvcBuilder builder)
    {
        return builder.AddApplicationPart(typeof(ServiceCollectionExtensions).Assembly);
    }
}