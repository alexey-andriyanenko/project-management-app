namespace Facade.BoardManagement.Contracts.Parameters.Board;

public class CreateBoardParameters
{
    public required string Name { get; set; } = string.Empty;
    
    public Guid BoardTypeId { get; set; }

    public Guid TenantId { get; set; }
    
    public Guid ProjectId { get; set; }
    
    public Guid UserId { get; set; }

    public IReadOnlyList<CreateBoardColumnItemParameters> Columns { get; set; } = [];
}

public class CreateBoardColumnItemParameters
{
    public required string Name { get; set; } = string.Empty;
}
