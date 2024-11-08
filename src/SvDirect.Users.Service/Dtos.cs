using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SvDirect.Users.Service.Dtos
{
    public record UserDto(Guid Id, [Required] string FirstName, [Required] string LastName, [Required][DataType(DataType.EmailAddress)] string Email, [Required] string PasswordHash, [Required] DateTime DateOfBirth, DateTimeOffset CreatedAt, DateTimeOffset UpdateAt);

    public record CreateUserDto([Required] string FirstName, [Required] string LastName, [Required][DataType(DataType.EmailAddress)] string Email, [Required] string Password, [Required] DateTime DateOfBirth);

    public record UpdateUserDto([Required] string FirstName, [Required] string LastName, [Required][DataType(DataType.EmailAddress)] string Email, [Required] string Password, [Required] DateTime DateOfBirth);

}