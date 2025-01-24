using Microsoft.EntityFrameworkCore;
using userpanel.api.Configurations;
using userpanel.api.Models;

namespace userpanel.api.Contexts;

public class UserPanelDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
    }

    public UserPanelDbContext(DbContextOptions<UserPanelDbContext> options) : base(options)
    {
        
    }
}