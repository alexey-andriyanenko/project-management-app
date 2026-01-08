namespace Facade.BoardManagement.Contracts.Parameters.Task;

public class DeleteTaskParameters
{
    public Guid Id { get; set; }
    
    public Guid TenantId { get; set; }
    
    public Guid ProjectId { get; set; }
    
    public Guid BoardId { get; set; }
}