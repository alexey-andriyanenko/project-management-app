using Microsoft.EntityFrameworkCore;
using Shared.EntityFrameworkCore.Configuration;

namespace Board.DataAccess;

public class BoardDbContextFactory : DesignTimeDbContextFactoryBase<BoardDbContext>
{
    protected override string GetConnectionStringName() => "BoardManagementDb";

    protected override BoardDbContext CreateNewInstance(DbContextOptions<BoardDbContext> options) =>
        new BoardDbContext(options);

    protected override void ConfigureOptions(DbContextOptionsBuilder<BoardDbContext> builder, string connectionString)
    {
        builder.UseNpgsql(connectionString);
    }
}