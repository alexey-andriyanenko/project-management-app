namespace Facade.BoardManagement.Contracts.Dtos;

public class TaskUserDto
{
    public Guid UserId { get; set; }
    
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public required string Email { get; set; }
}