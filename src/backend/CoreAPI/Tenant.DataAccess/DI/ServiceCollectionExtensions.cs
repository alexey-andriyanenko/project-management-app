using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tenant.DataAccess.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTenantDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TenantDbContext>((sp, options) =>
        {
            var connectionString = configuration.GetConnectionString("TenantManagementDb");
            
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly(typeof(TenantDbContext).Assembly.FullName);
            });
        });
        
        return services;
    }
}