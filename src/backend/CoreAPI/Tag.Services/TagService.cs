using Microsoft.EntityFrameworkCore;
using Tag.Contracts.Dtos;
using Tag.Contracts.Exceptions;
using Tag.Contracts.Parameters;
using Tag.Contracts.Result;
using Tag.Contracts.Services;
using Tag.DataAccess;
using Tag.Services.Mappings;

namespace Tag.Services;

public class TagService(TagDbContext dbContext) : ITagService
{
    public async Task<GetManyTagsByTenantIdResult> GetManyAsync(GetManyTagsByTenantIdParameters parameters)
    {
        var tagsQuery = dbContext.Tags.AsQueryable();
        
        if (parameters.ProjectId.HasValue) 
        {
            tagsQuery = tagsQuery.Where(t => t.TenantId == parameters.TenantId && t.ProjectId == parameters.ProjectId);
        }
        else
        {
            tagsQuery = tagsQuery.Where(t => t.TenantId == parameters.TenantId);
        }
        
        var tags = await tagsQuery.ToListAsync();

        return new GetManyTagsByTenantIdResult()
        {
            Tags = tags.Select(x => x.ToDto()).ToList()
        };
    }
    
    public async Task<GetManyTagsByIdsResult> GetManyAsync(GetManyTagsByIdsParameters parameters)
    {
        var tags = await dbContext.Tags
            .Where(t => t.TenantId == parameters.TenantId && parameters.TagIds.Contains(t.Id))
            .ToListAsync();

        return new GetManyTagsByIdsResult()
        {
            Tags = tags.Select(x => x.ToDto()).ToList()
        };
    }
    
    public async Task<TagDto> CreateAsync(CreateTagParameters parameters)
    {
        var newTag = new DataAccess.Entities.TagEntity()
        {
            TenantId = parameters.TenantId,
            ProjectId = parameters.ProjectId,
            Name = parameters.Name,
            Color = parameters.Color,
        };

        dbContext.Tags.Add(newTag);
        await dbContext.SaveChangesAsync();

        return newTag.ToDto();
    }
    
    public async Task<TagDto> UpdateAsync(UpdateTagParameters parameters)
    {
        var tag = await dbContext.Tags.FindAsync(parameters.Id, parameters.TenantId, parameters.Name, parameters.ProjectId);
        
        if (tag == null)
        {
            throw new TagNotFoundException(parameters.Id);
        }

        tag.Name = parameters.Name;
        tag.Color = parameters.Color;

        await dbContext.SaveChangesAsync();

        return tag.ToDto();
    }
    
    public async Task DeleteAsync(DeleteTagParameters parameters)
    {
        var tag = await dbContext.Tags.FindAsync(parameters.Id, parameters.TenantId);
        
        if (tag == null)
        {
            throw new TagNotFoundException(parameters.Id);
        }

        dbContext.Tags.Remove(tag);
        await dbContext.SaveChangesAsync();
    }
}