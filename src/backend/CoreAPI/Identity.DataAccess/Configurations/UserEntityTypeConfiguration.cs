using Identity.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.DataAccess.Configurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Id);
            
        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.HasIndex(x => x.UserName)
            .IsUnique();

        builder.HasIndex(x => new { x.FirstName, x.LastName });

        builder.Property(x => x.FirstName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasMaxLength(200)
            .IsRequired();
    }
}