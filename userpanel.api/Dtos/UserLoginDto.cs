using System.ComponentModel.DataAnnotations;

namespace userpanel.api.Dtos;

public class UserLoginDto
{
    [Required (ErrorMessage = "Username is required")]
    [StringLength(15, ErrorMessage = "Username must be between 5 and 15 characters", MinimumLength = 5)]
    public required string Username { get; set; }
    
    [Required (ErrorMessage = "Password is required")]
    [StringLength(15, ErrorMessage = "Password must be between 5 and 15 characters", MinimumLength = 5)]
    public required string Password { get; set; }
}