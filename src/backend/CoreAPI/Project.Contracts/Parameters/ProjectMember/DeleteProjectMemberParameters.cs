namespace Project.Contracts.Parameters.ProjectMember;

public class DeleteProjectMemberParameters
{
    public Guid TenantId { get; set; }
    
    public Guid ProjectId { get; set; }
    
    public Guid UserId { get; set; }
}