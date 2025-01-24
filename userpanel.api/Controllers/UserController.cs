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
    private readonly UserService _userService;

    public UserController(IUserRepository userRepository, UserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost ("/Register")]
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
                return Ok(new { message = "User created successfully"});
        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
        return BadRequest(new { message = "Unable to create user" });
    }

    [HttpPost ("/Login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        //Attempt to log the user in
        try
        {
            var user = await _userService.LoginAsync(userLoginDto);
            if (user != null) 
                return Ok(new { message = "Logged in successfully" });
        }
        catch (Exception ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        return BadRequest(new { message = "Unable to login." });
    }
}