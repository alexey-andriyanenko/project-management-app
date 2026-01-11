using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tag.Client.DI;
using Tag.DataAccess.DI;
using Tag.Events.DI;
using Tag.Services.DI;

namespace Tag.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTagModule(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddTagEvents()
            .AddTagDataAccess(configuration)
            .AddTagServices()
            .AddTagClient();
    }
}