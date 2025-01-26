﻿using userpanel.api.Dtos;
using userpanel.api.Models;

namespace userpanel.api.Extensions;

public static class UserExtension 
{
    public static UserRegistrationDto ToUserRegistrationDto(this User user)
    {
        return new UserRegistrationDto
        {
            Forename = user.Forename,
            Surname = user.Surname,
            Email = user.Email,
            Password = user.Password,
        };
    }

    public static UserRequestDto ToUserRequestDto(this User user)
    {
        return new UserRequestDto
        {
            UserId = user.UserId,
            Forename = user.Forename,
            Surname = user.Surname,
            Email = user.Email,
        };
    }

    public static UserLoginDto ToUserLoginDto(this User user)
    {
        return new UserLoginDto
        {
            Email = user.Email,
            Password = user.Password
        };
    }
}