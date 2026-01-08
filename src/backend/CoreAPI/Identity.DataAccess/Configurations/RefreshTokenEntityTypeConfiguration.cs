using Identity.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.DataAccess.Configurations;

public class RefreshTokenEntityTypeConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
{
    public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Token).IsRequired();
        
        builder.Property(x => x.ExpiresAt).IsRequired();
        
        builder.Property(x => x.CreatedAt).IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.RefreshTokens)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}