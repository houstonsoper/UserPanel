using Microsoft.AspNetCore.Identity;
using userpanel.api.Contexts;
using userpanel.api.Dtos;
using userpanel.api.Models;
using userpanel.api.Repositories;

namespace userpanel.api.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> CreateUserAsync(UserRegistrationDto userDto)
    {
        //Check if a user with the email already exists
        if (await _userRepository.GetUserByEmailAsync(userDto.Email) != null)
        {
            throw new Exception("A user with that email already exists");
        }
        
        //Hash the users password 
        var passwordHasher = new PasswordHasher<UserRegistrationDto>();
        var hashedPassword = passwordHasher.HashPassword(userDto, userDto.Password);
        
        // Create and save the user
        var user = new User
        {
            Forename = userDto.Forename,
            Surname = userDto.Surname,
            Email = userDto.Email,
            Password = hashedPassword,
        };
        
        return await _userRepository.CreateUserAsync(user);
    }

    public async Task<User?> LoginAsync(UserLoginDto userDto)
    {
        //Check if the user exists 
        var user = await _userRepository.GetUserByEmailAsync(userDto.Email);
        if (user == null) {
            throw new Exception("Invalid username or password");
        }
        
        //Check that their password is correct
        var passwordHasher = new PasswordHasher<User>();
        var result = passwordHasher.VerifyHashedPassword(user, user.Password, userDto.Password);

        if (result == PasswordVerificationResult.Failed)
        {
            throw new Exception("Invalid username or password");
        }

        return user;
    }
}