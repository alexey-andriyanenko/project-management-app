using Infrastructure.Database.MigrationsRunner.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Identity.DataAccess;

public class IdentityModuleMigrationsRunner(IdentityDbContext dbContext) : IModuleMigrationsRunner
{
    public string ModuleName => "IdentityModule";
    
    public async Task RunMigrationsAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.Database.MigrateAsync(cancellationToken);
    }
}
