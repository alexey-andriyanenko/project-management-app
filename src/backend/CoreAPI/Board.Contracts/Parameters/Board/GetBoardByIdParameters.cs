namespace Board.Contracts.Parameters.Board;

public class GetBoardByIdParameters
{
    public Guid Id { get; set; }
    
    public Guid TenantId { get; set; }
}