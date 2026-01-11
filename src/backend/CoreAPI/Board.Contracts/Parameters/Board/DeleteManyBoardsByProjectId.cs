namespace Board.Contracts.Parameters.Board;

public class DeleteManyBoardsByProjectId
{
    public required Guid TenantId { get; set; }
    
    public required Guid ProjectId { get; set; }
}