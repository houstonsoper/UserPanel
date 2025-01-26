﻿using Microsoft.EntityFrameworkCore;
using userpanel.api.Contexts;
using userpanel.api.Models;

namespace userpanel.api.Repositories;

public class PasswordTokenRepository : IPasswordTokenRepository
{
    private readonly UserPanelDbContext _context;

    public PasswordTokenRepository(UserPanelDbContext context)
    {
        _context = context;
    }
    
    public async Task<PasswordResetToken?> GetActiveTokenByUserIdAsync(Guid userId)
    {
        return await _context.PasswordResetTokens
            .FirstOrDefaultAsync(u => 
                u.UserId == userId && 
                u.TokenUsed == false && 
                u.ExpiresAt > DateTime.UtcNow); 
    }

    public async Task<PasswordResetToken?> GetTokenByTokenIdAsync(Guid tokenId)
    {
        return await _context.PasswordResetTokens
            .FirstOrDefaultAsync(u => u.TokenId == tokenId);
    }

    public async Task UpdateUsedTokenAsync(Guid tokenId)
    {
        var token = await _context.PasswordResetTokens.FindAsync(tokenId);

        if (token != null)
        {
            token.TokenUsed = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task AddTokenAsync(PasswordResetToken passwordResetToken)
    {
        await _context.PasswordResetTokens.AddAsync(passwordResetToken);
        await _context.SaveChangesAsync();
    }
}