using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tenant.DataAccess.Entities;
using Tenant.DataAccess.Enums;

namespace Tenant.DataAccess.Configuration;

public class TenantMemberEntityTypeConfiguration : IEntityTypeConfiguration<TenantMemberEntity>
{
   public void Configure(EntityTypeBuilder<TenantMemberEntity> builder)
   {
      builder.HasKey(x => new { x.TenantId, x.UserId });

      builder.Property(x => x.Role)
         .IsRequired()
         .HasConversion(
            x => x.ToString(),
            x => (TenantMemberRole)Enum.Parse(typeof(TenantMemberRole), x)
         );
   }
}