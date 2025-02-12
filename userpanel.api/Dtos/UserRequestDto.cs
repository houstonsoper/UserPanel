using userpanel.api.Models;

namespace userpanel.api.DTOs;

public class UserRequestDto
{
    public Guid UserId { get; set; }
    public required string Forename { get; set; }
    public required string Surname { get; set; }
    public string Email { get; set; } = string.Empty;
    
    public UserGroup? UserGroup { get; set; } 
}