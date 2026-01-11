using Board.Client.DI;
using Board.DataAccess.DI;
using Board.Events.DI;
using Board.Services.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Board.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBoardModule(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddBoardEvents()
            .AddBoardDataAccess(configuration)
            .AddBoardServices()
            .AddBoardClientServices();
            
        return services;
    }
}