using System.ComponentModel.DataAnnotations;

namespace userpanel.api.Dtos;

public class PasswordResetTokenPostDto
{
    [Required (ErrorMessage = "Email is required")]
    public required string Email { get; set; }
}