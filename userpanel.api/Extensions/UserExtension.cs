using userpanel.api.Dtos;
using userpanel.api.Models;

namespace userpanel.api.Extensions;

public static class UserExtension 
{
    public static UserPostDto ToUserPostDto(this User user)
    {
        return new UserPostDto
        {
            Username = user.Username,
            Email = user.Email,
            Password = user.Password
        };
    }

    public static UserRequestDto ToUserRequestDto(this User user)
    {
        return new UserRequestDto
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
            Password = user.Password
        };
    }
}