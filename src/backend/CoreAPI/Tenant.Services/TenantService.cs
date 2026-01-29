using Infrastructure.EventBus.Contracts;
using Microsoft.EntityFrameworkCore;
using Shared.String.Extensions;
using Tenant.Contracts.Dtos;
using Tenant.Contracts.Exceptions;
using Tenant.Contracts.Parameters;
using Tenant.Contracts.Results;
using Tenant.Contracts.Services;
using Tenant.DataAccess;
using Tenant.Events.Contracts;
using Tenant.Services.Mappings;
using TenantMemberRole = Tenant.DataAccess.Enums.TenantMemberRole;

namespace Tenant.Services;

public class TenantService(TenantDbContext dbContext, IEventBus eventBus) : ITenantService
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
        
        var ownerMember = await dbContext.TenantMembers
            .FirstOrDefaultAsync(tm => tm.TenantId == tenant.Id && tm.Role == TenantMemberRole.Owner);
        
        if (ownerMember == null)
        {
            throw new InvalidOperationException("Tenant owner member not found.");
        }
        
        return tenant.ToDto(ownerMember.UserId);
    }

    public async Task<GetManyTenantsByUserIdResult> GetManyAsync(GetManyTenantsByUserIdParameters parameters)
    {
        var tenantIds = await dbContext.TenantMembers
            .Where(tm => tm.UserId == parameters.UserId)
            .Select(tm => tm.TenantId)
            .ToListAsync();
        
        if (!tenantIds.Any())
        {
            return new GetManyTenantsByUserIdResult()
            {
                Tenants = new List<TenantDto>()
            };
        }
        
        var tenants = await dbContext.Tenants
            .Where(t => tenantIds.Contains(t.Id))
            .ToListAsync();

        var ownerMembers = await dbContext.TenantMembers
            .Where(tm => tenantIds.Contains(tm.TenantId) && tm.Role == TenantMemberRole.Owner)
            .ToListAsync();
        
        if (!ownerMembers.Any())
        {
            throw new InvalidOperationException("No owner members found for the tenants.");
        }
        
        var ownerMembersByTenantId = ownerMembers.ToDictionary(tm => tm.TenantId, tm => tm.UserId);
        
        return new GetManyTenantsByUserIdResult()
        {
            Tenants = tenants.Select(t => t.ToDto(ownerMembersByTenantId[t.Id])).ToList()
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
        
        var tenantMemberEntity = new DataAccess.Entities.TenantMemberEntity()
        {
            TenantId = tenantEntity.Id,
            UserId = parameters.UserId,
            Role = TenantMemberRole.Owner
        };

        dbContext.Tenants.Add(tenantEntity);
        dbContext.TenantMembers.Add(tenantMemberEntity);
        await dbContext.SaveChangesAsync();

        var ownerMember = await dbContext.TenantMembers
            .FirstOrDefaultAsync(tm => tm.TenantId == tenantEntity.Id && tm.Role == TenantMemberRole.Owner);
        
        if (ownerMember == null)
        {
            throw new InvalidOperationException("Tenant owner member not found.");
        }
        
        await eventBus.PublishAsync(new TenantCreatedEvent()
        {
            TenantId = tenantEntity.Id,
            OwnerUserId = parameters.UserId
        });
        
        return tenantEntity.ToDto(ownerMember.UserId);
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
        
        var ownerMember = await dbContext.TenantMembers
            .FirstOrDefaultAsync(tm => tm.TenantId == tenantEntity.Id && tm.Role == TenantMemberRole.Owner);
        
        if (ownerMember == null)
        {
            throw new InvalidOperationException("Tenant owner member not found.");
        }

        return tenantEntity.ToDto(ownerMember.UserId);
    }
    
    public async Task DeleteAsync(DeleteTenantParameters parameters)
    {
        var tenantEntity = await dbContext.Tenants
            .FirstOrDefaultAsync(t => t.Id == parameters.TenantId);

        if (tenantEntity == null)
        {
            throw new TenantNotFoundException(parameters.TenantId);
        }

        dbContext.Tenants.Remove(tenantEntity);
        await dbContext.SaveChangesAsync();
        
        await eventBus.PublishAsync(new TenantDeletedEvent() { 
            TenantId = parameters.TenantId 
        });
    }
}