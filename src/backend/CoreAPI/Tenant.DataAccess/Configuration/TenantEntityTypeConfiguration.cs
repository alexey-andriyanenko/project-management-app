using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tenant.DataAccess.Entities;

namespace Tenant.DataAccess.Configuration;

public class TenantEntityTypeConfiguration : IEntityTypeConfiguration<TenantEntity>
{
    public void Configure(EntityTypeBuilder<TenantEntity> builder) 
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id)
            .ValueGeneratedNever();
        
        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("name");
        
        builder.HasIndex(o => o.Name)
            .IsUnique();
        
        builder.HasIndex(o => o.Slug)
            .IsUnique();
    }
}