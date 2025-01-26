using userpanel.api.Models;

namespace userpanel.api.Repositories;

public interface IPasswordRepository
{
    Task<PasswordResetToken?> CreatePasswordResetTokenAsync(PasswordResetToken passwordResetToken);
    Task<PasswordResetToken?> GetPasswordResetTokenAsync(Guid userId);
}