using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Board.DataAccess.DI;

public static class ServicesCollectionExtensions
{
    public static IServiceCollection AddBoardDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BoardDbContext>((sp, options) =>
        {
            var connectionString = configuration.GetConnectionString("BoardManagementDb");
            
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly(typeof(BoardDbContext).Assembly.FullName);
            });
        });
        
        return services;
    }
}