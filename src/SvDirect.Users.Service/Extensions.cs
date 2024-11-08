using System;
using SvDirect.Users.Service.Dtos;
using SvDirect.Users.Service.Entities;

namespace SvDirect.Users.Service.Extensions
{
    public static class Extentions
    {
        public static UserDto AsDto(this User user)
        {
            return new UserDto(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                user.PasswordHash,
                user.DateOfBirth,
                user.CreatedAt,
                user.UpdateAt
            );
        }

        public static User AsUser(this CreateUserDto user)
        {
            return new User(
                Guid.NewGuid(),
                user.FirstName,
                user.LastName,
                user.Email,
                BCrypt.Net.BCrypt.HashPassword(user.Password),
                user.DateOfBirth,
                DateTimeOffset.UtcNow,
                DateTimeOffset.UtcNow
            );
        }

    }
}