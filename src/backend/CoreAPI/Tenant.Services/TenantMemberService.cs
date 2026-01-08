using Microsoft.EntityFrameworkCore;
using Tenant.Contracts.Dtos;
using Tenant.Contracts.Exceptions;
using Tenant.Contracts.Parameters;
using Tenant.Contracts.Results;
using Tenant.Contracts.Services;
using Tenant.DataAccess;
using Tenant.DataAccess.Enums;
using Tenant.Services.Mappings;

namespace Tenant.Services;

public class TenantMemberService(TenantDbContext dbContext) : ITenantMemberService
{
    public async Task<GetManyTenantMembersByTenantIdResult> GetManyAsync(GetManyTenantMembersByTenantIdParameters parameters)
    {
        var tenant = await dbContext.TenantMembers.FirstOrDefaultAsync(x => x.TenantId == parameters.TenantId);
        
        if (tenant == null)
        {
            throw new TenantNotFoundException(parameters.TenantId);
        }
        
        var tenantMembers = await dbContext.TenantMembers
            .Where(tm => tm.TenantId == parameters.TenantId)
            .ToListAsync();

        return new GetManyTenantMembersByTenantIdResult()
        {
            TenantMembers = tenantMembers.Select(x => x.ToDto()).ToList()
        };
    }

    public async Task<TenantMemberDto> CreateAsync(CreateTenantMemberParameters parameters)
    {
        var tenant = await dbContext.TenantMembers.FirstOrDefaultAsync(x => x.TenantId == parameters.TenantId);
        
        if (tenant == null)
        {
            throw new TenantNotFoundException(parameters.TenantId);
        }
        
        var tenantMemberEntity = new DataAccess.Entities.TenantMemberEntity()
        {
            TenantId = parameters.TenantId,
            UserId = parameters.UserId,
            Role = (DataAccess.Enums.TenantMemberRole)parameters.Role
        };
        
        dbContext.TenantMembers.Add(tenantMemberEntity);
        await dbContext.SaveChangesAsync();
        
        return tenantMemberEntity.ToDto();
    }
    
    public async Task<TenantMemberDto> UpdateAsync(UpdateTenantMemberParameters parameters)
    {
        var tenantMemberEntity = await dbContext.TenantMembers
            .FirstOrDefaultAsync(tm => tm.TenantId == parameters.TenantId && tm.UserId == parameters.UserId);

        if (tenantMemberEntity == null)
        {
            throw new TenantMemberNotFoundException(parameters.TenantId, parameters.UserId);
        }

        tenantMemberEntity.Role = (DataAccess.Enums.TenantMemberRole)parameters.Role;

        dbContext.TenantMembers.Update(tenantMemberEntity);
        await dbContext.SaveChangesAsync();

        return tenantMemberEntity.ToDto();
    }
    
    public async Task DeleteAsync(DeleteTenantMemberParameters parameters)
    {
        var tenantMemberEntity = await dbContext.TenantMembers
            .FirstOrDefaultAsync(tm => tm.TenantId == parameters.TenantId && tm.UserId == parameters.UserId);

        if (tenantMemberEntity == null)
        {
            throw new TenantMemberNotFoundException(parameters.TenantId, parameters.UserId);
        }

        dbContext.TenantMembers.Remove(tenantMemberEntity);
        await dbContext.SaveChangesAsync();
    }
}