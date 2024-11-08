using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SvDirect.Users.Service.Dtos;
using BCrypt;

namespace SvDirect.Users.Service.Controllers
{

    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {

        private static readonly List<UserDto> users = new()
        {
            new UserDto(Guid.NewGuid(), "Sizwe", "Mbatha", "SizweMbatha@hotmail.com", BCrypt.Net.BCrypt.HashPassword("password123"), DateTime.Now.AddYears(22), DateTime.Parse("2024-11-08T05:20:36.887Z"), DateTime.Parse("2024-11-08T05:28:39.887Z")),
            new UserDto(Guid.NewGuid(), "Bulani", "Mongezwe", "BulaniMongezwe@hotmail.com", BCrypt.Net.BCrypt.HashPassword("password123"), DateTime.Now.AddYears(17), DateTime.Parse("2024-11-08T05:23:36.887Z"), DateTime.Parse("2024-11-08T05:29:44.887Z")),
            new UserDto(Guid.NewGuid(), "Thumbulami", "Mjoxa", "ThumbulamiMjoxa@outlook.com", BCrypt.Net.BCrypt.HashPassword("password123"), DateTime.Now.AddYears(29), DateTime.Parse("2024-11-08T05:17:36.887Z"), DateTime.Parse("2024-11-08T05:30:36.887Z")),
        };

        [HttpGet]
        public IEnumerable<UserDto> GetAll()
        {
            return users;
        }

        [HttpGet("{id}")]
        public ActionResult<UserDto> GetById(Guid id)
        {
            var user = users.Where(t => t.Id == id).FirstOrDefault();
            if (user == null)
            {
                return NotFound("user not found");
            }
            return user;
        }

        [HttpPost]
        public ActionResult CreateUser(CreateUserDto user)
        {

            var newUser = new UserDto(Guid.NewGuid(), user.FirstName, user.LastName, user.Email, BCrypt.Net.BCrypt.HashPassword(user.Password), user.DateOfBirth, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow);

            users.Add(newUser);

            return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, user);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUser(Guid id, UpdateUserDto user)
        {

            var existingUser = users.Where(t => t.Id == id).SingleOrDefault();

            if (existingUser == null)
            {
                return NotFound("user not found");
            }

            var updateditem = existingUser with
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password),
                DateOfBirth = user.DateOfBirth,
                UpdateAt = DateTimeOffset.UtcNow
            };

            var index = users.FindIndex(y => y.Id == existingUser.Id);
            if (index < 0)
            {
                return NotFound("index of user not found");
            }
            users[index] = updateditem;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(Guid id)
        {
            var index = users.FindIndex(y => y.Id == id);
            if (index < 0)
            {
                return NotFound("index of user not found");
            }
            users.RemoveAt(index);

            return NoContent();
        }
    }
}