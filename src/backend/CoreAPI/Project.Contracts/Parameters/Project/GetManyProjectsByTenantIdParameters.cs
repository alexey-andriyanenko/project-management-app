namespace Project.Contracts.Parameters.Project;

public class GetManyProjectsByTenantIdParameters
{
    public Guid UserId { get; set; }
    
    public Guid TenantId { get; set; }
}