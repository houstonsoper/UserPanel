using userpanel.api.Models;
using userpanel.api.Repositories;

namespace userpanel.api.Services;

public class PasswordTokenService : IPasswordTokenService
{
    private readonly IPasswordRepository _passwordRepository;

    public PasswordTokenService(IPasswordRepository passwordRepository)
    {
        _passwordRepository = passwordRepository;
    }
    
    public async Task<PasswordResetToken?> CreatePasswordResetTokenAsync(PasswordResetToken token)
    {
        //Check if there is already an active token for the user
        var existingToken = await _passwordRepository.GetPasswordResetTokenAsync(token.UserId);

        //If no existing token create a new one
        if (existingToken == null)
        { 
            return await _passwordRepository.CreatePasswordResetTokenAsync(token);
        }
        
        return existingToken;
    }
}