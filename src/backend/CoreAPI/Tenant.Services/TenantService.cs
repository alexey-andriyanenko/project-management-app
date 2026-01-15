using Microsoft.EntityFrameworkCore;
using Shared.String.Extensions;
using Tenant.Contracts.Dtos;
using Tenant.Contracts.Exceptions;
using Tenant.Contracts.Parameters;
using Tenant.Contracts.Results;
using Tenant.Contracts.Services;
using Tenant.DataAccess;
using Tenant.Services.Mappings;

namespace Tenant.Services;

public class TenantService(TenantDbContext dbContext) : ITenantService
{
    public async Task<TenantDto> GetAsync(GetTenantBySlugParameters parameters)
    {
        var tenant = await dbContext.Tenants
            .FirstOrDefaultAsync(t => t.Slug == parameters.Slug);
        
        if (tenant == null)
        {
            throw new TenantNotFoundException(parameters.Slug);
        }
        
        var member = await dbContext.TenantMembers
            .FirstOrDefaultAsync(tm => tm.TenantId == tenant!.Id && tm.UserId == parameters.MemberId);

        if (member == null)
        {
            throw new TenantNotFoundException(parameters.Slug);
        }
        
        return tenant.ToDto();
    }

    public async Task<GetManyTenantsByUserIdResult> GetManyAsync(GetManyTenantsByUserIdParameters parameters)
    {
        var tenantIds = await dbContext.TenantMembers
            .Where(tm => tm.UserId == parameters.UserId)
            .Select(tm => tm.TenantId)
            .ToListAsync();
        
        var tenants = await dbContext.Tenants
            .Where(t => tenantIds.Contains(t.Id))
            .ToListAsync();

        return new GetManyTenantsByUserIdResult()
        {
            Tenants = tenants.Select(t => t.ToDto()).ToList()
        };
    }
    
    public async Task<TenantDto> CreateAsync(CreateTenantParameters parameters)
    {
        var tenantEntity = new DataAccess.Entities.TenantEntity()
        {
            Id = Guid.NewGuid(),
            Name = parameters.Name,
            Slug = parameters.Name.ToSlug(),
        };

        dbContext.Tenants.Add(tenantEntity);
        await dbContext.SaveChangesAsync();

        return tenantEntity.ToDto();
    }
    
    public async Task<TenantDto> UpdateAsync(UpdateTenantParameters parameters)
    {
        var tenantEntity = await dbContext.Tenants
            .FirstOrDefaultAsync(t => t.Id == parameters.Id);

        if (tenantEntity == null)
        {
            throw new TenantNotFoundException(parameters.Id);
        }

        tenantEntity.Name = parameters.Name;
        tenantEntity.Slug = parameters.Name.ToSlug();

        dbContext.Tenants.Update(tenantEntity);
        await dbContext.SaveChangesAsync();

        return tenantEntity.ToDto();
    }
}