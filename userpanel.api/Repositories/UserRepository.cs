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
    public async Task<User?> CreateUser(User user)
    {
        try
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating user: {ex.Message}");
            return null;
        }
    }
}