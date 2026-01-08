using Board.Contracts.Dtos;
using Identity.Contracts.Dtos;
using Tag.Contracts.Dtos;

namespace Facade.BoardManagement.Services.Mappings.Task;

public static class DtosMappings
{
    public static Facade.BoardManagement.Contracts.Dtos.TaskDto ToFacadeDto(this TaskDto dto, IReadOnlyList<TagDto> tags, UserDto createdByUser, UserDto? assignedToUser)
    {
        return new Facade.BoardManagement.Contracts.Dtos.TaskDto()
        {
            Id = dto.Id,
            TenantId = dto.TenantId,
            ProjectId = dto.ProjectId,
            BoardId = dto.BoardId,
            BoardColumnId = dto.BoardColumnId,
            Title = dto.Title,
            Description = dto.Description,
            CreatedBy = createdByUser.ToFacadeDto(),
            AssignedTo = assignedToUser?.ToFacadeDto(),
            Tags = tags.Select(tag => new Facade.BoardManagement.Contracts.Dtos.TaskTagDto()
            {
                TagId = tag.Id,
                Name = tag.Name,
                Color = tag.Color
            }).ToList(),
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt
        };
    }
    
    private static Facade.BoardManagement.Contracts.Dtos.TaskUserDto ToFacadeDto(this UserDto dto)
    {
        return new Facade.BoardManagement.Contracts.Dtos.TaskUserDto()
        {
            UserId = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email
        };
    }
}