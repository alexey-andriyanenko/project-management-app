namespace Facade.IdentityManagement.Contracts.Exceptions;

public class InvitationNotFoundException(Guid id) : Exception($"Invitation with Id '{id}' was not found.")
{
}
