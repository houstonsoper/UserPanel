using userpanel.api.Models;

namespace userpanel.api.Services;

public interface IPasswordTokenService
{
    public Task<PasswordResetToken?> CreatePasswordResetTokenAsync(PasswordResetToken token);
}