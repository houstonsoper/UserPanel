using System.ComponentModel.DataAnnotations;

namespace userpanel.api.Dtos;

public class UserLoginDto
{
    [Required (ErrorMessage = "Email is required")]
    [StringLength(254, ErrorMessage = "Email must be between 5 and 254 characters", MinimumLength = 5)]
    public required string Email { get; set; }
    
    [Required (ErrorMessage = "Password is required")]
    [StringLength(15, ErrorMessage = "Password must be between 5 and 15 characters", MinimumLength = 5)]
    public required string Password { get; set; }
}