using Infrastructure.Database.MigrationsRunner.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Tag.DataAccess;

public class TagModuleMigrationsRunner(TagDbContext dbContext) : IModuleMigrationsRunner
{
    public string ModuleName => "TagModule";
    
    public async Task RunMigrationsAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.Database.MigrateAsync(cancellationToken);
    }
}
