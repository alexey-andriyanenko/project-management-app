using Board.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Board.DataAccess.Configurations;

public class TaskTagEntityTypeConfiguration : IEntityTypeConfiguration<TaskTagEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TaskTagEntity> builder)
    {
        builder.HasKey(x => new { x.TaskId, x.TagId });

        builder.HasOne(x => x.Task)
            .WithMany(x => x.Tags)
            .HasForeignKey(x => x.TaskId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.ToTable("TaskTags");
    }
}
