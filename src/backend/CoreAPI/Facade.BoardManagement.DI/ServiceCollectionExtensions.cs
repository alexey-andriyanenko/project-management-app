using Board.DI;
using Facade.BoardManagement.Services.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Facade.BoardManagement.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFacadeBoardManagementModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddBoardModule(configuration);
        services.AddFacadeBoardManagementServices();
        
        return services;
    }
    
    public static IMvcBuilder AddFacadeBoardManagementControllers(this IMvcBuilder builder)
    {
        return builder.AddApplicationPart(typeof(ServiceCollectionExtensions).Assembly);
    }
}