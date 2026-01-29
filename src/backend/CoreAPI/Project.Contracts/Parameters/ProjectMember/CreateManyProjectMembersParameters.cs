using Project.Contracts.Dtos;

namespace Project.Contracts.Parameters.ProjectMember;

public class CreateManyProjectMembersParameters
{
    public Guid TenantId { get; set; }
    
    public Guid ProjectId { get; set; }
    
    public IReadOnlyList<CreateProjectMemberItemParameters> Members { get; set; } = [];
}

public class CreateProjectMemberItemParameters
{
    public Guid UserId { get; set; }
    
    public ProjectMemberRole Role { get; set; }
}
