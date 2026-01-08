namespace Board.Contracts.Parameters.Board;

public class GetManyBoardsByProjectIdParameters
{
    public Guid ProjectId { get; set; }
    
    public Guid TenantId { get; set; }
}