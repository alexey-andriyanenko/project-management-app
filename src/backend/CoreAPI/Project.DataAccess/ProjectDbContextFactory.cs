using Microsoft.EntityFrameworkCore;
using Shared.EntityFrameworkCore.Configuration;

namespace Project.DataAccess;

public class ProjectDbContextFactory : DesignTimeDbContextFactoryBase<ProjectDbContext>
{
    protected override string GetConnectionStringName() => "ProjectManagementDb";

    protected override ProjectDbContext CreateNewInstance(DbContextOptions<ProjectDbContext> options) => new ProjectDbContext(options);

    protected override void ConfigureOptions(DbContextOptionsBuilder<ProjectDbContext> builder, string connectionString)
    {
        builder.UseNpgsql(connectionString);
    }
}