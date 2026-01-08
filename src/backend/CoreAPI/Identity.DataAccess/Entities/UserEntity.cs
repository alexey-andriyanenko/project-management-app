using Microsoft.AspNetCore.Identity;

namespace Identity.DataAccess.Entities;

public class UserEntity : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public ICollection<RefreshTokenEntity> RefreshTokens { get; set; } = [];
}