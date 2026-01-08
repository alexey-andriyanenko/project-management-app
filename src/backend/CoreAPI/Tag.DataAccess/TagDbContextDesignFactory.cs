using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Shared.EntityFrameworkCore.Configuration;

namespace Tag.DataAccess;

public class TagDbContextDesignFactory : DesignTimeDbContextFactoryBase<TagDbContext>
{
    protected override string GetConnectionStringName() => "TagManagementDb";

    protected override TagDbContext CreateNewInstance(DbContextOptions<TagDbContext> options) => new TagDbContext(options);

    protected override void ConfigureOptions(DbContextOptionsBuilder<TagDbContext> builder, string connectionString)
    {
        builder.UseNpgsql(connectionString);
    }
}