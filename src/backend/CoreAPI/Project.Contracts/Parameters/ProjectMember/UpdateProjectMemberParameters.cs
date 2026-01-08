using Project.Contracts.Dtos;

namespace Project.Contracts.Parameters.ProjectMember;

public class UpdateProjectMemberParameters
{
    public Guid TenantId { get; set; }
    
    public Guid ProjectId { get; set; }
    
    public Guid UserId { get; set; }
    
    public ProjectMemberRole Role { get; set; }
}