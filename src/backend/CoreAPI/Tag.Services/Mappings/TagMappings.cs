using Tag.Contracts.Dtos;

namespace Tag.Services.Mappings;

public static class TagMappings
{
    public static TagDto ToDto(this DataAccess.Entities.TagEntity entity)
    {
        return new TagDto()
        {
            Id = entity.Id,
            TenantId = entity.TenantId,
            ProjectId = entity.ProjectId,
            Name = entity.Name,
            Color = entity.Color,
        };
    }
}