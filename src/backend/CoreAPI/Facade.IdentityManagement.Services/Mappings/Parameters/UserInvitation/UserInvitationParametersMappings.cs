namespace Facade.IdentityManagement.Services.Mappings.Parameters.UserInvitation;

public static class UserInvitationParametersMappings
{
    public static Identity.Contracts.Parameters.UserInvite.GetManyUserInvitationsParameters ToCoreParameters(this Facade.IdentityManagement.Contracts.Parameters.UserInvitation.GetManyUserInvitationsParameters parameters)
    {
        return new Identity.Contracts.Parameters.UserInvite.GetManyUserInvitationsParameters
        {
            TenantId = parameters.TenantId
        };
    }
    
    public static Identity.Contracts.Parameters.UserInvite.CreateUserInvitationParameters ToCoreParameters(this Facade.IdentityManagement.Contracts.Parameters.UserInvitation.CreateUserInvitationParameters parameters)
    {
        return new Identity.Contracts.Parameters.UserInvite.CreateUserInvitationParameters
        {
            TenantId = parameters.TenantId,
            Email = parameters.Email,
            FirstName = parameters.FirstName,
            LastName = parameters.LastName,
            TenantMemberRole = parameters.TenantMemberRole
        };
    }
    
    public static Identity.Contracts.Parameters.UserInvite.AcceptUserInvitationParameters ToCoreParameters(this Facade.IdentityManagement.Contracts.Parameters.UserInvitation.AcceptUserInvitationParameters parameters)
    {
        return new Identity.Contracts.Parameters.UserInvite.AcceptUserInvitationParameters
        {
            InvitationToken = parameters.InvitationToken,
            UserName = parameters.UserName,
            Password = parameters.Password
        };
    }
    
    public static Identity.Contracts.Parameters.UserInvite.ResendUserInvitationParameters ToCoreParameters(this Facade.IdentityManagement.Contracts.Parameters.UserInvitation.ResendUserInvitationParameters parameters)
    {
        return new Identity.Contracts.Parameters.UserInvite.ResendUserInvitationParameters
        {
            InvitationId = parameters.InvitationId
        };
    }
    
    public static Identity.Contracts.Parameters.UserInvite.ValidateUserInvitationParameters ToCoreParameters(this Facade.IdentityManagement.Contracts.Parameters.UserInvitation.ValidateUserInvitationParameters parameters)
    {
        return new Identity.Contracts.Parameters.UserInvite.ValidateUserInvitationParameters
        {
            InvitationToken = parameters.InvitationToken
        };
    }
    
    public static Identity.Contracts.Parameters.UserInvite.DeleteUserInvitationParameters ToCoreParameters(this Facade.IdentityManagement.Contracts.Parameters.UserInvitation.DeleteUserInvitationParameters parameters)
    {
        return new Identity.Contracts.Parameters.UserInvite.DeleteUserInvitationParameters
        {
            InvitationId = parameters.InvitationId
        };
    }
}
