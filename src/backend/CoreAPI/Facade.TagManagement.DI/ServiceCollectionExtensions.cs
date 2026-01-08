using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tag.DI;
using Facade.TagManagement.Services.DI;

namespace Facade.TagManagement.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFacadeTagManagementModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTagModule(configuration);
        services.AddFacadeTagManagementServices();

        return services;
    }
    
    public static IMvcBuilder AddFacadeTagManagementControllers(this IMvcBuilder builder)
    {
        return builder.AddApplicationPart(typeof(ServiceCollectionExtensions).Assembly);
    }
}