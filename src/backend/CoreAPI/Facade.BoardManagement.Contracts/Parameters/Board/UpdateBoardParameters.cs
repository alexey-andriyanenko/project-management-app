namespace Facade.BoardManagement.Contracts.Parameters.Board;

public class UpdateBoardParameters
{
    public Guid Id { get; set; }
    
    public Guid TenantId { get; set; }
    
    public Guid ProjectId { get; set; }
    
    public Guid UserId { get; set; }
    
    public required string Name { get; set; } = string.Empty;

    public IReadOnlyList<UpdateBoardColumnItemParameters> Columns { get; set; } = [];    
}

public class UpdateBoardColumnItemParameters
{
    public required string Name { get; set; } = string.Empty;
}
