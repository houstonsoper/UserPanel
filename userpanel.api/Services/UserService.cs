using Microsoft.AspNetCore.Identity;
using userpanel.api.Contexts;
using userpanel.api.Dtos;
using userpanel.api.Models;
using userpanel.api.Repositories;

namespace userpanel.api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailSender _emailSender;

    public UserService(IUserRepository userRepository, IEmailSender emailSender)
    {
        _userRepository = userRepository;
        _emailSender = emailSender;
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
        
        var newUser =  await _userRepository.CreateUserAsync(user);

        if (newUser == null)
        {
            throw new Exception("Failed to create user");
        }
        //Send email to user that they have been registered on a seperate thread
        _ = Task.Run(() => _emailSender.SendRegistrationEmail(userDto.Email, "User created"));
        
        return newUser;
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

    public async Task<User?> GetUserByIdAsync(Guid userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        return user;
    }

    public async Task ResetPasswordAsync(Guid userId, string newPassword)
    {
        //Get user
        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        if (user.Password.Length < 5 || user.Password.Length > 15)
        {
            throw new Exception ("Invalid password");
        }
        
        //Hash password
        var passwordHasher = new PasswordHasher<User>();
        var hashedPassword = passwordHasher.HashPassword(user, newPassword);
        
        var resetPassword = await _userRepository.ResetPasswordAsync(user, hashedPassword);

        if (!resetPassword)
        {
            throw new Exception("Failed to reset password");
        }
    }
}