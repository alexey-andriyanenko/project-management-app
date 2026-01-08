namespace Facade.BoardManagement.Contracts.Parameters.Task;

public class GetManyTasksByBoardIdParameters
{
    public Guid TenantId { get; set; }
    
    public Guid ProjectId { get; set; }
    
    public Guid BoardId { get; set; }
}