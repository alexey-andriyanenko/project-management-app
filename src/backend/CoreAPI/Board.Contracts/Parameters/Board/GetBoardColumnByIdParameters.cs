namespace Board.Contracts.Parameters.Board;

public class GetBoardColumnByIdParameters
{
    public Guid TenantId { get; set; }
    
    public Guid ProjectId { get; set; }
    
    public Guid BoardId { get; set; }
    
    public Guid BoardColumnId { get; set; }
}