namespace Facade.BoardManagement.Contracts.Parameters.BoardType;

public class GetManyBoardTypesByTenantIdParameters
{
    public Guid UserId { get; set; }
    
    public Guid TenantId { get; set; }
}