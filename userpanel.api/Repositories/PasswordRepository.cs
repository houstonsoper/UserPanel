using Microsoft.EntityFrameworkCore;
using userpanel.api.Contexts;
using userpanel.api.Models;

namespace userpanel.api.Repositories;

public class PasswordRepository : IPasswordRepository
{
    private readonly UserPanelDbContext _context;

    public PasswordRepository(UserPanelDbContext context)
    {
        _context = context;
    }
    
    public async Task<PasswordResetToken?> GetPasswordResetTokenAsync(Guid userId)
    {
        return await _context.PasswordResetTokens
            .FirstOrDefaultAsync(u => 
                u.UserId == userId && 
                u.TokenUsed == false && 
                u.ExpiresAt > DateTime.UtcNow); 
    }
    
    public async Task<PasswordResetToken?> CreatePasswordResetTokenAsync(PasswordResetToken passwordResetToken)
    {
        await _context.PasswordResetTokens.AddAsync(passwordResetToken);
        await _context.SaveChangesAsync();
        return passwordResetToken;
    }
}