namespace Facade.BoardManagement.Contracts.Parameters.Board;

public class DeleteBoardParameters
{
    public Guid Id { get; set; }
    
    public Guid TenantId { get; set; }
}