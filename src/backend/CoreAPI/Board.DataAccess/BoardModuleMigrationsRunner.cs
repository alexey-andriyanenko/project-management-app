using Infrastructure.Database.MigrationsRunner.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Board.DataAccess;

public class BoardModuleMigrationsRunner(BoardDbContext dbContext) : IModuleMigrationsRunner
{
    public string ModuleName => "BoardModule";
    
    public async Task RunMigrationsAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.Database.MigrateAsync(cancellationToken);
    }
}
