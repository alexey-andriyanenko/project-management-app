using Facade.ProjectManagement.Contracts.Dtos;

namespace Facade.ProjectManagement.Contracts.Parameters.ProjectMember;

public class CreateManyProjectMembersParameters
{
    public Guid UserId { get; set; }
    
    public Guid TenantId { get; set; }
    
    public Guid ProjectId { get; set; }
    
    public IReadOnlyList<CreateProjectMemberItemParameters> Members { get; set; } = [];
}

public class CreateProjectMemberItemParameters
{
    public Guid UserId { get; set; }
    
    public ProjectMemberRole Role { get; set; }
}
