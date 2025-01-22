using Microsoft.EntityFrameworkCore;
using userpanel.api.Models;

namespace userpanel.api.Contexts;

public class UserPanelDbContext : DbContext
{
    DbSet<User> Users { get; set; }
    
    public UserPanelDbContext(DbContextOptions<UserPanelDbContext> options) : base(options)
    {
        
    }
}