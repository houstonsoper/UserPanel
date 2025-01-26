using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using userpanel.api.Dtos;
using userpanel.api.Extensions;
using userpanel.api.Models;
using userpanel.api.Repositories;
using userpanel.api.Services;

namespace userpanel.api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost ("Register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationDto userDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        //Attempt to create the user
        try
        {
            var user = await _userService.CreateUserAsync(userDto);
            
            if (user != null)
            { 
                //Store session info
                HttpContext.Session.SetString("UserId", user.UserId.ToString()); 
                
                return Ok(new { message = "User created successfully" });
            }
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message});
        }
        return BadRequest(new { message = "Unable to create user" });
    }

    [HttpPost ("Login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        //Attempt to log the user in
        try
        {
            var user = await _userService.LoginAsync(userLoginDto);
            
            if (user == null)
            {
                return BadRequest(new { message = "Unable to login" });
            } 
            
            //Store session info
            HttpContext.Session.SetString("UserId", user.UserId.ToString()); 
            
            return Ok(new { message = "Login successful" });
        }
        catch (Exception ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
    [HttpGet]
    public async Task<IActionResult> GetUser()
    {
        var userId = HttpContext.Session.GetString("UserId");

        if (userId == null)
        {
            return Unauthorized(new {message = "User is not logged in"});
        }
        
        try
        {
            var user = await _userService.GetUserByIdAsync(Guid.Parse(userId)); 
            return Ok(user.ToUserRequestDto());
        }
        catch (Exception ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpPost("Logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("UserId");
        return Ok(new { message = "Logout successful" });
    }
    
    [HttpPost("PasswordResetToken")]
    public async Task<IActionResult> PasswordResetToken([FromBody] CreatePasswordResetTokenDto tokenDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        //Attempt to create the token
        try
        {
            var token = await _userService.CreatePasswordResetTokenAsync(tokenDto.ToPasswordResetToken());

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

    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword()
    {
        return Ok(new { message = "Password reset successful" });
    }
}