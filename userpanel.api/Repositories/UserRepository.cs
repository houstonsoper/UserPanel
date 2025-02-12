﻿using Microsoft.EntityFrameworkCore;
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
    
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users
            .Include(u => u.UserGroup)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetUserByIdAsync(Guid userId)
    {
        return await _context.Users
            .Include(u => u.UserGroup)
            .FirstOrDefaultAsync(u => u.UserId == userId);
    }
    
    
    public async Task<bool> ResetPasswordAsync(User user, string hashedPassword)
    {
        //Update password and save changes to DB
        user.Password = hashedPassword;
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task UpdateUsersGroupAsync(User user, int groupId)
    {
        user.UserGroupId = groupId;
        await _context.SaveChangesAsync();
    }

    public async Task <UserGroup?> GetUserGroupById(int groupId)
    {
        return await _context.UserGroups.FindAsync(groupId);
    }

    public IQueryable<User> GetAllUsersQuery()
    {
        return _context.Users
            .Include(u => u.UserGroup)
            .AsQueryable();
    }

    public async Task DeleteUserAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}