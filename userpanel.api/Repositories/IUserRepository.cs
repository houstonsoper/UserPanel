using userpanel.api.Models;

namespace userpanel.api.Repositories;

public interface IUserRepository
{
    Task<User?> CreateUserAsync(User user);
    Task <User?> GetUserByUsernameAsync(string username);
    Task <User?> GetUserByEmailAsync(string email);
}
