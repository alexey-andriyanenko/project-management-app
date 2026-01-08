using Tenant.Contracts.Dtos;
using Tenant.DataAccess.Entities;

namespace Tenant.Services.Mappings;

public static class TenantMappings
{
    public static TenantDto ToDto(this TenantEntity tenant)
    {
        return new TenantDto
        {
            Id = tenant.Id,
            Name = tenant.Name,
            Slug = tenant.Slug,
        };
    }
}