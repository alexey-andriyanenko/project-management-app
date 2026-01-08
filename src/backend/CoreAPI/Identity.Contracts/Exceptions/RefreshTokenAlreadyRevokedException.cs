namespace Identity.Contracts.Exceptions;

public class RefreshTokenAlreadyRevokedException() : Exception("Refresh token has already been revoked.")
{
    
}