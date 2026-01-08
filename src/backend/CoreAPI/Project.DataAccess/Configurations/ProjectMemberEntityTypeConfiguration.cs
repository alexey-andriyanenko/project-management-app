using Microsoft.EntityFrameworkCore;
using Project.DataAccess.Entities;
using Project.DataAccess.Enums;

namespace Project.DataAccess.Configurations;

public class ProjectMemberEntityTypeConfiguration : IEntityTypeConfiguration<ProjectMemberEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProjectMemberEntity> builder)
    {
        builder.HasKey(x => new { x.UserId, x.ProjectId });

        builder.Property(x => x.Role)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => (ProjectMemberRole)Enum.Parse(typeof(ProjectMemberRole), v));
    }
}