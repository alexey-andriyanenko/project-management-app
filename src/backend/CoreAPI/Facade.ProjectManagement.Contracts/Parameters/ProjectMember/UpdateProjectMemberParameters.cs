using Facade.ProjectManagement.Contracts.Dtos;

namespace Facade.ProjectManagement.Contracts.Parameters.ProjectMember;

public class UpdateProjectMemberParameters
{
    public Guid UserId { get; set; }
    
    public Guid TenantId { get; set; }
    
    public Guid ProjectId { get; set; }
    
    public Guid MemberUserId { get; set; }
    
    public ProjectMemberRole Role { get; set; }
}