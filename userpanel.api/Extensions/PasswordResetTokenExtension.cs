using userpanel.api.Dtos;
using userpanel.api.Models;

namespace userpanel.api.Extensions;

public static class PasswordResetTokenExtension
{
   public static PasswordResetTokenRequestDto ToPasswordResetTokenRequestDto(this PasswordResetToken token)
   {
      return new PasswordResetTokenRequestDto
      {
         TokenId = token.TokenId,
         UserId = token.UserId,
         CreatedAt = token.CreatedAt,
         ExpiresAt = token.ExpiresAt,
         TokenUsed = token.TokenUsed
      }; 
   }

   public static CreatePasswordResetTokenDto ToCreatePasswordResetTokenDto(this PasswordResetToken token)
   {
      return new CreatePasswordResetTokenDto
      {
         UserId = token.UserId,
      };
   }

   public static PasswordResetToken ToPasswordResetToken(this CreatePasswordResetTokenDto token)
   {
      return new PasswordResetToken
      {
         TokenId = Guid.NewGuid(),
         UserId = token.UserId,
         CreatedAt = DateTime.UtcNow,
         ExpiresAt = DateTime.UtcNow.AddMinutes(30),
         TokenUsed = false
      };
   }
}
