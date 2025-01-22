using userpanel.api.Contexts;
using userpanel.api.Models;

namespace userpanel.api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserPanelDbContext _context;

    public UserRepository(UserPanelDbContext context)
    {
        _context = context;
    }
    public Task<User?> CreateUser(User user)
    {
        throw new NotImplementedException();
    }
}