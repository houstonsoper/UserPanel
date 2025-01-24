using System.ComponentModel.DataAnnotations;

namespace userpanel.api.Models;

public class User
{
    public Guid UserId { get; set; } = Guid.NewGuid();
    
    [Required (ErrorMessage = "Username is required")]
    [StringLength(15, ErrorMessage = "Username must be between 5 and 15 characters", MinimumLength = 5)]
    public required string Username { get; set; }
    
    [Required (ErrorMessage = "Email is required")]
    [StringLength(254, ErrorMessage = "Email must be between 5 and 254 characters", MinimumLength = 5)]
    public required string Email { get; set; }
    
    [Required (ErrorMessage = "Password is required")]
    [StringLength(15, ErrorMessage = "Password must be between 5 and 15 characters", MinimumLength = 5)]
    public required string Password { get; set; }
}