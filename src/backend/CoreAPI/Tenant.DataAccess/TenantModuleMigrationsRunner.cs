using Infrastructure.Database.MigrationsRunner.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Tenant.DataAccess;

public class TenantModuleMigrationsRunner(TenantDbContext dbContext) : IModuleMigrationsRunner
{
    public string ModuleName => "TenantModule";
    
    public async Task RunMigrationsAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.Database.MigrateAsync(cancellationToken);
    }
}
