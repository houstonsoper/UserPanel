using System.ComponentModel.DataAnnotations;

namespace userpanel.api.Dtos;

public class CreatePasswordResetTokenDto
{
    [Required (ErrorMessage = "UserId is required")]
    public required Guid UserId { get; set; }
}