using userpanel.api.Models;

namespace userpanel.api.Repositories;

public interface IUserRepository
{
    Task<User?> CreateUser(User user);
}
