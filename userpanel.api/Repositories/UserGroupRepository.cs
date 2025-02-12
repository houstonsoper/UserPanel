using userpanel.api.Contexts;
using userpanel.api.Models;
using Microsoft.EntityFrameworkCore;

namespace userpanel.api.Repositories;

public class UserGroupRepository : IUserGroupRepository
{
    private readonly UserPanelDbContext _context;

    public UserGroupRepository(UserPanelDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<UserGroup>> GetAllUserGroups()
    {
        return await _context.UserGroups.ToListAsync();
    }
}