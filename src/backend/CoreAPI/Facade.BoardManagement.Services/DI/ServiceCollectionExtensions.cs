using Facade.BoardManagement.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Facade.BoardManagement.Services.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFacadeBoardManagementServices(this IServiceCollection services)
    {
        services.AddScoped<IBoardManagementService, BoardManagementService>();
        services.AddScoped<ITaskManagementService, TaskManagementService>();
        services.AddScoped<IBoardTypeManagementService, BoardTypeManagementService>();
        
        return services;
    }
}