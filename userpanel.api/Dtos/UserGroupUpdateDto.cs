using System.ComponentModel.DataAnnotations;

namespace userpanel.api.DTOs;

public class UserGroupUpdateDto
{
    [Required (ErrorMessage = "User id is required")]
    public int GroupId { get; set; }
}