﻿using System.ComponentModel.DataAnnotations;

namespace userpanel.api.Dtos;

public class UserLoginDto
{
    [Required (ErrorMessage = "Email is required")]
    public required string Email { get; set; }
    
    [Required (ErrorMessage = "Password is required")]
    public required string Password { get; set; }
}