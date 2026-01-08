using Microsoft.EntityFrameworkCore;
using Shared.EntityFrameworkCore.Configuration;

namespace Identity.DataAccess;

public class IdentityDbContextFactory : DesignTimeDbContextFactoryBase<IdentityDbContext>
{
    protected override string GetConnectionStringName() => "IdentityManagementDb";
    
    protected override IdentityDbContext CreateNewInstance(DbContextOptions<IdentityDbContext> options)
        => new IdentityDbContext(options);

    protected override void ConfigureOptions(DbContextOptionsBuilder<IdentityDbContext> builder, string connectionString)
    {
        builder.UseNpgsql(connectionString);
    }
}