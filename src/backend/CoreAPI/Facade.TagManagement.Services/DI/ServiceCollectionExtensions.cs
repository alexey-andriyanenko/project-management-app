using Facade.TagManagement.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Facade.TagManagement.Services.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFacadeTagManagementServices(this IServiceCollection services)
    {
        services.AddScoped<ITagManagementService, TagManagementService>();
        
        return services;
    }
}