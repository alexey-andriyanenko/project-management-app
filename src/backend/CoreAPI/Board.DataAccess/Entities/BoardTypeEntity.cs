namespace Board.DataAccess.Entities;

public class BoardTypeEntity
{
    public Guid Id { get; set; }
    
    public Guid TenantId { get; set; }
    
    public Guid? ProjectId { get; set; }
    
    public required string Name { get; set; }
    
    // Indicates if the board type is essential and cannot be deleted
    public bool IsEssential { get; set; }
    
    public ICollection<BoardEntity> Boards { get; set; } = [];
}
