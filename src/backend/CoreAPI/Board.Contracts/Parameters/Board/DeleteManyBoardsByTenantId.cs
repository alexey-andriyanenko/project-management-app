namespace Board.Contracts.Parameters.Board;

public class DeleteManyBoardsByTenantId
{
    public required Guid TenantId { get; set; }
}