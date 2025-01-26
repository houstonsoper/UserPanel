using Microsoft.AspNetCore.Mvc;
using userpanel.api.Dtos;
using userpanel.api.Extensions;
using userpanel.api.Repositories;
using userpanel.api.Services;

namespace userpanel.api.Controllers;

[ApiController]
[Route("[controller]")]

public class PasswordResetTokenController : Controller
{
    private readonly IPasswordTokenService _passwordService;

    public PasswordResetTokenController(IPasswordTokenService passwordService)
    {
        _passwordService = passwordService;
    }
    
    [HttpPost]
    public async Task<IActionResult> PasswordResetToken([FromBody] CreatePasswordResetTokenDto tokenDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        //Attempt to create the token
        try
        {
            var token = await _passwordService.CreatePasswordResetTokenAsync(tokenDto.ToPasswordResetToken());

            if (token != null)
            {
                return Ok(token.ToPasswordResetTokenRequestDto());
            }
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        return BadRequest(new { message = "Unable to create token" });
    }
}