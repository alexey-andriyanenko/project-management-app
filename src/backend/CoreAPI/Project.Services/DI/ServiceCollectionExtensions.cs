using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Contracts.Service;

namespace Project.Services.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProjectServices(this IServiceCollection services)
    {
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IProjectMemberService, ProjectMemberService>();
        
        return services;
    }
}