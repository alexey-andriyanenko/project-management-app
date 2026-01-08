namespace Facade.ProjectManagement.Services.Mappings.Dtos;

public static class DtosMappings
{
    public static Facade.ProjectManagement.Contracts.Dtos.ProjectDto ToFacadeDto(this Project.Contracts.Dtos.ProjectDto project)
    {
        return new Facade.ProjectManagement.Contracts.Dtos.ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            Slug = project.Slug,
            Description = project.Description,
            CreatedAt = project.CreatedAt,
            UpdatedAt = project.UpdatedAt
        };
    }
    
    public static Facade.ProjectManagement.Contracts.Dtos.ProjectMemberDto ToFacadeDto(this Project.Contracts.Dtos.ProjectMemberDto projectMember, Identity.Contracts.Dtos.UserDto user)
    {
        return new Facade.ProjectManagement.Contracts.Dtos.ProjectMemberDto
        {
            ProjectId = projectMember.ProjectId,
            UserId = projectMember.UserId,
            Role = (Facade.ProjectManagement.Contracts.Dtos.ProjectMemberRole)projectMember.Role,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };
    }
}