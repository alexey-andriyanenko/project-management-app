using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Client.DI;
using Project.DataAccess.DI;
using Project.Services.DI;

namespace Project.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProjectModule(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddProjectDataAccess(configuration)
            .AddProjectServices()
            .AddProjectClient();
        
        return services;
    }
}