namespace Identity.Contracts.Exceptions;

public class InvitationAlreadyExistsException(string email, Guid tenantId) 
    : Exception($"An invitation for email '{email}' already exists in tenant '{tenantId}'.")
{
}
