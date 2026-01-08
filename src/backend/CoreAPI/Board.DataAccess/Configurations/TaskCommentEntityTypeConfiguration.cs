using Board.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Board.DataAccess.Configurations;

public class TaskCommentEntityTypeConfiguration : IEntityTypeConfiguration<TaskCommentEntity>
{
    public void Configure(EntityTypeBuilder<TaskCommentEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ContentAsJson)
            .IsRequired()
            .HasColumnType("jsonb");

        builder.Property(x => x.ContentAsPlainText)
            .IsRequired();
        
        builder.HasIndex(x => x.ContentAsPlainText);
        
        builder.Property(x => x.CreatedByUserId)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .ValueGeneratedOnUpdate();

        builder.Property(x => x.TaskId)
            .IsRequired();

        builder.HasOne<TaskEntity>()
            .WithMany(t => t.Comments)
            .HasForeignKey(c => c.TaskId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}