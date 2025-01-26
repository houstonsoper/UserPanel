using userpanel.api.Models;

namespace userpanel.api.Repositories;

public interface IUserRepository
{
    Task<User?> CreateUserAsync(User user);
    Task <User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByIdAsync(Guid userId);
    Task<PasswordResetToken?> CreatePasswordResetTokenAsync(PasswordResetToken passwordResetToken);
    Task<PasswordResetToken?> GetPasswordResetTokenAsync(Guid userId);
    Task<bool> ResetPasswordAsync (User user, string hashedPassword);
}
