using Microsoft.EntityFrameworkCore;
using Project.DataAccess.Configurations;
using Project.DataAccess.Entities;

namespace Project.DataAccess;

public class ProjectDbContext(DbContextOptions<ProjectDbContext> options) : DbContext(options)
{
    public DbSet<ProjectEntity> Projects { get; set; }
    
    public DbSet<ProjectMemberEntity> ProjectMembers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ProjectEntityTypeConfiguration());
        builder.ApplyConfiguration(new ProjectMemberEntityTypeConfiguration());
        
        base.OnModelCreating(builder);
    }
}