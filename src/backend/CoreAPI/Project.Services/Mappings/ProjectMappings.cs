using Project.Contracts.Dtos;

namespace Project.Services.Mappings;

public static class ProjectMappings
{
    public static ProjectDto ToDto(this DataAccess.Entities.ProjectEntity project)
    {
        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            Slug = project.Slug,
            Description = project.Description,
            CreatedAt = project.CreatedAt,
            UpdatedAt = project.UpdatedAt,
        };
    }
    
}