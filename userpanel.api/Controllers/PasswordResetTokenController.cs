﻿using Microsoft.AspNetCore.Mvc;
using userpanel.api.DTOs;
using userpanel.api.Extensions;
using userpanel.api.Models;
using userpanel.api.Repositories;
using userpanel.api.Services;

namespace userpanel.api.Controllers;

[ApiController]
[Route("[controller]")]

public class PasswordResetTokenController : Controller
{
    private readonly IPasswordTokenService _passwordTokenService;
    private readonly IUserService _userService;

    public PasswordResetTokenController(IPasswordTokenService passwordTokenService, IUserService userService)
    {
        _passwordTokenService = passwordTokenService;
        _userService = userService;
    }
    
    [HttpPost]
    public async Task<IActionResult> PasswordResetToken([FromBody] PasswordResetTokenPostDto resetTokenDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        //Attempt to create the PasswordResetToken
        try
        { 
            var token = await _passwordTokenService.CreatePasswordResetTokenAsync(resetTokenDto.Email);

            if (token != null)
            {
                return Ok(token.ToPasswordResetTokenRequestDto());
            }
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        return BadRequest(new { message = "Unable to create tokenPost" });
    }
    
    [HttpGet ("{tokenId}")]
    public async Task<IActionResult> PasswordResetToken([FromRoute] string tokenId)
    {
        //Attempt to retrieve the PasswordTokenDetails
        try
        { 
            var token = await _passwordTokenService.GetTokenByTokenIdAsync(new Guid(tokenId));

            if (token != null)
            {
                return Ok(token.ToPasswordResetTokenRequestDto());
            }
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        return BadRequest(new { message = "Unable to create tokenPost" });
    }
}