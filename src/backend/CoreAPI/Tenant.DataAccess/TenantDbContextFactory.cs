using Microsoft.EntityFrameworkCore;
using Shared.EntityFrameworkCore.Configuration;

namespace Tenant.DataAccess;

public class TenantDbContextFactory : DesignTimeDbContextFactoryBase<TenantDbContext>
{
    protected override string GetConnectionStringName() => "TenantManagementDb";

    protected override TenantDbContext CreateNewInstance(DbContextOptions<TenantDbContext> options) => new TenantDbContext(options);

    protected override void ConfigureOptions(DbContextOptionsBuilder<TenantDbContext> builder, string connectionString)
    {
        builder.UseNpgsql(connectionString);
    }
}