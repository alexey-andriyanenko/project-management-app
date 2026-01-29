namespace Facade.IdentityManagement.Contracts.Exceptions;

public class UserAlreadyExistsException(string email) : Exception($"A user with email '{email}' already exists.")
{
}
