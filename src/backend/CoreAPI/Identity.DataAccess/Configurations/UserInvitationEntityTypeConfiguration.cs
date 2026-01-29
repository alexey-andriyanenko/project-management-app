using Identity.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.DataAccess.Configurations;

public class UserInvitationEntityTypeConfiguration : IEntityTypeConfiguration<UserInvitationEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserInvitationEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Token)
            .IsRequired()
            .HasMaxLength(256);
        
        builder.HasIndex(x => x.Token)
            .IsUnique();
        
        builder.Property(x => x.TenantId)
            .IsRequired();
        
        builder.HasIndex(x => new { x.TenantId, x.Email })
            .IsUnique();
        
        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(256);
        
        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(x => x.TenantMemberRole)
            .HasConversion<string>(
                v => v.ToString(),
                v => (Enums.TenantMemberRole)Enum.Parse(typeof(Enums.TenantMemberRole), v))
            .IsRequired();
        
        builder.Property(x => x.CreatedAt)
            .IsRequired();
    }
}