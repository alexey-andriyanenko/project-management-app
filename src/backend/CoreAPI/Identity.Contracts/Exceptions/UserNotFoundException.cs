namespace Identity.Contracts.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(Guid id) : base($"User with Id '{id}' was not found.")
    {
    }
    
    public UserNotFoundException(string email) : base($"User with Email '{email}' was not found.")
    {
    }
}