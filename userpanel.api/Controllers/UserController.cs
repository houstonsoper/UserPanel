using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using userpanel.api.Dtos;
using userpanel.api.Extensions;
using userpanel.api.Models;
using userpanel.api.Repositories;

namespace userpanel.api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserPostDto userPostDto)
    {
        var passwordHasher = new PasswordHasher<object>();
        var hashedPassword = passwordHasher.HashPassword(userPostDto.Username ,userPostDto.Password);

        var newUser = new User
        {
            Username = userPostDto.Username,
            Email = userPostDto.Email,
            Password = hashedPassword,
        };
        
        var user = await _userRepository.CreateUser(newUser);

        if (user == null)
        {
            return BadRequest("Unable to create user");
        }
        
        return Ok(user.ToUserRequestDto());
    }
}