using Infrastructure.Database.MigrationsRunner.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tenant.DataAccess.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTenantDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TenantDbContext>((sp, options) => {
            var connectionString = configuration["TenantManagementService:DbConnection"];
            
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly(typeof(TenantDbContext).Assembly.FullName);
            });
        });

        services.AddScoped<IModuleMigrationsRunner, TenantModuleMigrationsRunner>();
        
        return services;
    }
}