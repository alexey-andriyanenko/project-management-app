namespace Facade.BoardManagement.Contracts.Dtos;

public class TaskUserDto
{
    public Guid UserId { get; set; }
    
    public required string FullName { get; set; }
    
    public required string Email { get; set; }
}