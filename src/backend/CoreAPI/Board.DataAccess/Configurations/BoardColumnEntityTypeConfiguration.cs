using Board.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Board.DataAccess.Configurations;

public class BoardColumnEntityTypeConfiguration : IEntityTypeConfiguration<BoardColumnEntity>
{
    public void Configure(EntityTypeBuilder<BoardColumnEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => new { x.Name, x.BoardId })
            .IsUnique();
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(x => x.Order)
            .IsRequired();
        
        builder.Property(x => x.CreatedByUserId)
            .IsRequired();
        
        builder.Property(x => x.CreatedAt)
            .IsRequired();
        
        builder.HasOne(x => x.Board)
            .WithMany(x => x.Columns)
            .HasForeignKey(x => x.BoardId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}