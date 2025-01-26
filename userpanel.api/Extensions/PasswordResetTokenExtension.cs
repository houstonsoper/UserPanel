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
}