using Facade.IdentityManagement.Services.DI;
using Identity.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Facade.IdentityManagement.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFacadeIdentityManagementModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddIdentityModule(configuration);
        services.AddIdentityManagementFacadeServices();

        return services;
    }
    
    public static IMvcBuilder AddFacadeIdentityManagementControllers(this IMvcBuilder builder)
    {
        return builder.AddApplicationPart(typeof(ServiceCollectionExtensions).Assembly);
    }
}