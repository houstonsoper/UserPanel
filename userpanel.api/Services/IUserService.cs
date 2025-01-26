using userpanel.api.Dtos;
using userpanel.api.Models;

namespace userpanel.api.Services;

public interface IUserService
{
    public Task<User?> CreateUserAsync(UserRegistrationDto userDto);
    public Task<User?> LoginAsync(UserLoginDto userDto);
    public Task<User?> GetUserByIdAsync(Guid userId);
    public Task ResetPasswordAsync(Guid userId, string hashedPassword);
}