namespace userpanel.api.Dtos;

public class PasswordResetTokenRequestDto
{
    public Guid TokenId { get; set; } 
    public Guid UserId { get; set; } 
    public DateTime CreatedAt { get; set; } 
    public DateTime ExpiresAt { get; set; } 
    public bool TokenUsed { get; set; } 
}