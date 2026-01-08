using Facade.ProjectManagement.Services.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.DI;

namespace Facade.ProjectManagement.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFacadeProjectManagementModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddProjectModule(configuration);
        services.AddFacadeProjectManagementServices();

        return services;
    }

    public static IMvcBuilder AddFacadeProjectManagementControllers(this IMvcBuilder builder)
    {
        return builder.AddApplicationPart(typeof(ServiceCollectionExtensions).Assembly);
    }
}