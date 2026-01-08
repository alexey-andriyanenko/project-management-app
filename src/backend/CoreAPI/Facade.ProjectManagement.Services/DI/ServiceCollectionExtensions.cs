using Facade.ProjectManagement.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Facade.ProjectManagement.Services.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFacadeProjectManagementServices(this IServiceCollection services)
    {
        services.AddScoped<IProjectManagementService, ProjectManagementService>();
        services.AddScoped<IProjectMemberManagementService, ProjectMemberManagementService>();
        
        return services;
    }
}