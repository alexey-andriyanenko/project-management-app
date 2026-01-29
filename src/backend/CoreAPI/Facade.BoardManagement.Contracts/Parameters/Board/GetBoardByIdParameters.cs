namespace Facade.BoardManagement.Contracts.Parameters.Board;

public class GetBoardByIdParameters
{
    public Guid Id { get; set; }
    
    public Guid TenantId { get; set; }
    
    public Guid ProjectId { get; set; }
}