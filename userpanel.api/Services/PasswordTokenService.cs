using userpanel.api.Models;
using userpanel.api.Repositories;

namespace userpanel.api.Services;

public class PasswordTokenService : IPasswordTokenService
{
    private readonly IPasswordRepository _passwordRepository;
    private readonly IEmailSender _emailSender;
    private readonly IUserService _userService;

    public PasswordTokenService(IPasswordRepository passwordRepository, IEmailSender emailSender, IUserService userService)
    {
        _passwordRepository = passwordRepository;
        _emailSender = emailSender;
        _userService = userService;
    }
    
    public async Task<PasswordResetToken?> CreatePasswordResetTokenAsync(PasswordResetToken token)
    {
        //Get the details of the user that the password token is for
        var user = await _userService.GetUserByIdAsync(token.UserId);

        if (user == null)
        {
            throw new Exception($"User {token.UserId} not found");
        }
        
        //Check if there is already an active token for the user
        var existingToken = await _passwordRepository.GetPasswordResetTokenAsync(user.UserId);

        //Return existing token if it exists
        if (existingToken != null)
        {
            //Send email to user
            _ = Task.Run(() => _emailSender.SendPasswordResetEmail(user.Email, existingToken.TokenId));
            
            return existingToken;
        } 
        
        //Otherwise create a new one
        var newToken = await _passwordRepository.CreatePasswordResetTokenAsync(token); 
        
        if (newToken == null) 
        { 
            throw new ApplicationException("Could not create new password reset token"); 
        } 
        
        //Send email to user
        _ = Task.Run(() => _emailSender.SendPasswordResetEmail(user.Email, newToken.TokenId));
        
        return newToken;
    }
}