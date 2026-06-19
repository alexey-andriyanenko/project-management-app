using Infrastructure.Database.MigrationsRunner.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Project.DataAccess;

public class ProjectModuleMigrationsRunner(ProjectDbContext dbContext) : IModuleMigrationsRunner
{
    public string ModuleName => "ProjectModule";
    
    public async Task RunMigrationsAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.Database.MigrateAsync(cancellationToken);
    }
}
