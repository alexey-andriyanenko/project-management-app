namespace Project.Contracts.Parameters.ProjectMember;

public class GetManyProjectMembersByProjectIdParameters
{
    public Guid TenantId { get; set; }
    
    public Guid ProjectId { get; set; }
}