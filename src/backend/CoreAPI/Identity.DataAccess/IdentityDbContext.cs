using Identity.DataAccess.Configurations;
using Identity.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.DataAccess;

public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<RefreshTokenEntity> RefreshTokens { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserEntityTypeConfiguration());
        builder.ApplyConfiguration(new RefreshTokenEntityTypeConfiguration());
        
        base.OnModelCreating(builder);
    }
}