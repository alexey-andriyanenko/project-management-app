using Board.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Board.DataAccess.Configurations;

public class BoardTypeEntityTypeConfiguration : IEntityTypeConfiguration<BoardTypeEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<BoardTypeEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.TenantId)
            .IsRequired();
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasIndex(x => x.Name)
            .IsUnique();
        
        builder.Property(x => x.IsEssential)
            .IsRequired();
        
        builder.HasMany(x => x.Boards)
            .WithOne(x => x.BoardType)
            .HasForeignKey(x => x.BoardTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}