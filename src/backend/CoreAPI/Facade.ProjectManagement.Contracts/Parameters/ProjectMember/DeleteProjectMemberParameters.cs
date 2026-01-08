namespace Facade.ProjectManagement.Contracts.Parameters.ProjectMember;

public class DeleteProjectMemberParameters
{
    public Guid UserId { get; set; }
    
    public Guid TenantId { get; set; }
    
    public Guid ProjectId { get; set; }
    
    public Guid MemberUserId { get; set; }
}