using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.DataAccess.Entities;

namespace Project.DataAccess.Configurations;

public class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<ProjectEntity>
{
    public void Configure(EntityTypeBuilder<ProjectEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.HasIndex(x => new { x.TenantId, x.Name, x.Slug })
            .IsUnique();

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Slug)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(x => x.TenantId)
            .IsRequired();

        builder.HasMany(x => x.Members)
            .WithOne()
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);
    }
}