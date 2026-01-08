namespace Facade.TagManagement.Services.Mappings;

public static class DtosMappings
{
    public static Facade.TagManagement.Contracts.Dtos.TagDto ToFacadeDto(this Tag.Contracts.Dtos.TagDto dto)
    {
        return new Facade.TagManagement.Contracts.Dtos.TagDto
        {
            Id = dto.Id,
            TenantId = dto.TenantId,
            ProjectId = dto.ProjectId,
            Name = dto.Name,
            Color = dto.Color
        };
    }
}