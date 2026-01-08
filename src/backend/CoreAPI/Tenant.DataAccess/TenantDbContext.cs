using Microsoft.EntityFrameworkCore;
using Tenant.DataAccess.Configuration;
using Tenant.DataAccess.Entities;

namespace Tenant.DataAccess;

public class TenantDbContext : DbContext
{
    public TenantDbContext(DbContextOptions<TenantDbContext> options) :
        base(options)
    { }
    
    public DbSet<TenantEntity> Tenants { get; set; }
    
    public DbSet<TenantMemberEntity> TenantMembers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new TenantEntityTypeConfiguration());
        builder.ApplyConfiguration(new TenantMemberEntityTypeConfiguration());
        
        base.OnModelCreating(builder);
    }
}