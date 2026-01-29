using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tag.DataAccess.Entities;

namespace Tag.DataAccess.Configurations;

public class TagEntityTypeConfiguration : IEntityTypeConfiguration<TagEntity>
{
    public void Configure(EntityTypeBuilder<TagEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => new { x.TenantId, x.Name })
            .IsUnique();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.TenantId)
            .IsRequired();
        
        builder.Property(x => x.Color)
            .HasMaxLength(7)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");


        builder.Property(x => x.UpdatedAt)
            .ValueGeneratedOnUpdate()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        builder.ToTable("Tags");
    }
}