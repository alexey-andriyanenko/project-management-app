namespace Identity.DataAccess.Entities;

public class RefreshTokenEntity
{
    public Guid Id { get; set; }
    
    public string Token { get; set; } = string.Empty;
    
    public DateTime ExpiresAt { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? RevokedAt { get; set; }
    
    public string? ReplacedByToken { get; set; }
    
    public Guid UserId { get; set; }
    
    public UserEntity? User { get; set; }
    
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    
    public bool IsRevoked => RevokedAt != null;
}