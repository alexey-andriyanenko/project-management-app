using Microsoft.EntityFrameworkCore;
using Tag.Contracts.Dtos;
using Tag.Contracts.Exceptions;
using Tag.Contracts.Parameters;
using Tag.Contracts.Result;
using Tag.Contracts.Services;
using Tag.DataAccess;
using Tag.DataAccess.Entities;
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
        var tag = await dbContext.Tags.FindAsync(parameters.Id, parameters.TenantId, parameters.Name,
            parameters.ProjectId);

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

    public async Task DeleteManyAsync(DeleteManyTagsByTenantId parameters, CancellationToken cancellationToken)
    {
        await dbContext.Tags
            .Where(t => t.TenantId == parameters.TenantId)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task DeleteManyAsync(DeleteManyTagsByProjectId parameters, CancellationToken cancellationToken)
    {
        await dbContext.Tags
            .Where(t => t.TenantId == parameters.TenantId && t.ProjectId == parameters.ProjectId)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task SeedTagsForProjectAsync(SeedTagsForProjectParameters parameters,
        CancellationToken cancellationToken)
    {
        var tags = new List<TagEntity>
        {
            new TagEntity
            {
                Id = Guid.NewGuid(),
                TenantId = parameters.TenantId,
                ProjectId = parameters.ProjectId,
                Name = "Release v1.0",
                Color = "#2E8B57",
                CreatedAt = DateTime.UtcNow
            },
            new TagEntity
            {
                Id = Guid.NewGuid(),
                TenantId = parameters.TenantId,
                ProjectId = parameters.ProjectId,
                Name = "Release vNext",
                Color = "#20B2AA",
                CreatedAt = DateTime.UtcNow
            },
            new TagEntity
            {
                Id = Guid.NewGuid(),
                TenantId = parameters.TenantId,
                ProjectId = parameters.ProjectId,
                Name = "Frontend",
                Color = "#FF69B4",
                CreatedAt = DateTime.UtcNow
            },
            new TagEntity
            {
                Id = Guid.NewGuid(),
                TenantId = parameters.TenantId,
                ProjectId = parameters.ProjectId,
                Name = "Backend",
                Color = "#6495ED",
                CreatedAt = DateTime.UtcNow
            },
            new TagEntity
            {
                Id = Guid.NewGuid(),
                TenantId = parameters.TenantId,
                ProjectId = parameters.ProjectId,
                Name = "API",
                Color = "#00BFFF",
                CreatedAt = DateTime.UtcNow
            },
            new TagEntity
            {
                Id = Guid.NewGuid(),
                TenantId = parameters.TenantId,
                ProjectId = parameters.ProjectId,
                Name = "Documentation",
                Color = "#8B4513",
                CreatedAt = DateTime.UtcNow
            },
            new TagEntity
            {
                Id = Guid.NewGuid(),
                TenantId = parameters.TenantId,
                ProjectId = parameters.ProjectId,
                Name = "UI/UX",
                Color = "#FF1493",
                CreatedAt = DateTime.UtcNow
            },
            new TagEntity
            {
                Id = Guid.NewGuid(),
                TenantId = parameters.TenantId,
                ProjectId = parameters.ProjectId,
                Name = "Testing",
                Color = "#32CD32",
                CreatedAt = DateTime.UtcNow
            },
            new TagEntity
            {
                Id = Guid.NewGuid(),
                TenantId = parameters.TenantId,
                ProjectId = parameters.ProjectId,
                Name = "Sprint 1",
                Color = "#FFDAB9",
                CreatedAt = DateTime.UtcNow
            },
            new TagEntity
            {
                Id = Guid.NewGuid(),
                TenantId = parameters.TenantId,
                ProjectId = parameters.ProjectId,
                Name = "Sprint 2",
                Color = "#E6E6FA",
                CreatedAt = DateTime.UtcNow
            }
        };

        await dbContext.Tags.AddRangeAsync(tags, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task SeedTagsForTenantAsync(SeedTagsForTenantParameters parameters,
        CancellationToken cancellationToken)
    {
        var tags = new List<TagEntity>
        {
            new TagEntity
            {
                Id = Guid.NewGuid(),
                TenantId = parameters.TenantId,
                ProjectId = null,
                Name = "Urgent",
                Color = "#FF0000",
                CreatedAt = DateTime.UtcNow
            },
            new TagEntity
            {
                Id = Guid.NewGuid(),
                TenantId = parameters.TenantId,
                ProjectId = null,
                Name = "High Priority",
                Color = "#FFA500",
                CreatedAt = DateTime.UtcNow
            },
            new TagEntity
            {
                Id = Guid.NewGuid(),
                TenantId = parameters.TenantId,
                ProjectId = null,
                Name = "Medium Priority",
                Color = "#FFD700",
                CreatedAt = DateTime.UtcNow
            },
            new TagEntity
            {
                Id = Guid.NewGuid(),
                TenantId = parameters.TenantId,
                ProjectId = null,
                Name = "Low Priority",
                Color = "#008000",
                CreatedAt = DateTime.UtcNow
            },
            new TagEntity
            {
                Id = Guid.NewGuid(),
                TenantId = parameters.TenantId,
                ProjectId = null,
                Name = "Bug",
                Color = "#DC143C",
                CreatedAt = DateTime.UtcNow
            },
            new TagEntity
            {
                Id = Guid.NewGuid(),
                TenantId = parameters.TenantId,
                ProjectId = null,
                Name = "Feature",
                Color = "#800080",
                CreatedAt = DateTime.UtcNow
            },
            new TagEntity
            {
                Id = Guid.NewGuid(),
                TenantId = parameters.TenantId,
                ProjectId = null,
                Name = "Improvement",
                Color = "#4682B4",
                CreatedAt = DateTime.UtcNow
            },
            new TagEntity
            {
                Id = Guid.NewGuid(),
                TenantId = parameters.TenantId,
                ProjectId = null,
                Name = "Support",
                Color = "#00CED1",
                CreatedAt = DateTime.UtcNow
            },
            new TagEntity
            {
                Id = Guid.NewGuid(),
                TenantId = parameters.TenantId,
                ProjectId = null,
                Name = "Blocked",
                Color = "#000000",
                CreatedAt = DateTime.UtcNow
            },
            new TagEntity
            {
                Id = Guid.NewGuid(),
                TenantId = parameters.TenantId,
                ProjectId = null,
                Name = "Needs Review",
                Color = "#1E90FF",
                CreatedAt = DateTime.UtcNow
            }
        };

        await dbContext.Tags.AddRangeAsync(tags, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}