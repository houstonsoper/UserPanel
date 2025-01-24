using Microsoft.EntityFrameworkCore;
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

    public async Task<User?> CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        return user;
    }
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }
}