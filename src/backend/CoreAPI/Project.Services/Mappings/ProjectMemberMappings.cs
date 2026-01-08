using Project.Contracts.Dtos;

namespace Project.Services.Mappings;

public static class ProjectMemberMappings
{
    public static ProjectMemberDto ToDto(this DataAccess.Entities.ProjectMemberEntity entity)
    {
        return new ProjectMemberDto
        {
            ProjectId = entity.ProjectId,
            UserId = entity.UserId,
            Role = (ProjectMemberRole)entity.Role
        };
    }
}