using Microsoft.Extensions.DependencyInjection;
using Project.Client.Contracts;
using Project.Client.Contracts.Resources;
using Project.Client.Resources;

namespace Project.Client.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProjectClient(this IServiceCollection services)
    {
        services.AddScoped<IProjectResource, ProjectResource>();
        services.AddScoped<IProjectMemberResource, ProjectMemberResource>();
        services.AddScoped<IProjectClient, ProjectClient>();
        
        return services;
    }
}