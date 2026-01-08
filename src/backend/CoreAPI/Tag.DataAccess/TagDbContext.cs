using Microsoft.EntityFrameworkCore;
using Tag.DataAccess.Configurations;
using Tag.DataAccess.Entities;

namespace Tag.DataAccess;

public class TagDbContext(DbContextOptions<TagDbContext> options) : DbContext(options)
{
    public DbSet<TagEntity> Tags { get;set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new TagEntityTypeConfiguration());
        
        base.OnModelCreating(builder);
    }
}