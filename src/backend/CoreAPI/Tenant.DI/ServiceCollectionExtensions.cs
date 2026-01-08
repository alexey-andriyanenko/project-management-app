using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tenant.Client.DI;
using Tenant.DataAccess.DI;
using Tenant.Services.DI;

namespace Tenant.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTenantModule(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddTenantDataAccess(configuration)
            .AddTenantServices()
            .AddTenantClient();
        
        return services;
    }
}