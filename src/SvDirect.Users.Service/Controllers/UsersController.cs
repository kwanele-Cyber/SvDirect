using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SvDirect.Users.Service.Dtos;
using SvDirect.Users.Service.Extensions;
using SvDirect.Users.Service.Entities;
using SvDirect.Common;

namespace SvDirect.Users.Service.Controllers
{

    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {

        private readonly IRepository<User> usersRepository;

        public UsersController(IRepository<User> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            return (await usersRepository.GetUsersAllAsync())
            .Select(item => item.AsDto());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetByIdAsync(Guid id)
        {
            var user = await usersRepository.GetUserAsync(id);

            if (user == null)
            {
                return NotFound("user not found");
            }
            return user.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult> CreateUserAsync(CreateUserDto user)
        {
            User newUser = user.AsUser();
            await usersRepository.CreateUserAsync(newUser);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = newUser.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(Guid id, UpdateUserDto user)
        {

            var existingItem = await usersRepository.GetUserAsync(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.FirstName = user.FirstName;
            existingItem.LastName = user.LastName;
            existingItem.Email = user.Email;
            existingItem.DateOfBirth = user.DateOfBirth;
            existingItem.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            existingItem.UpdateAt = DateTimeOffset.UtcNow;

            await usersRepository.UpdateAsync(existingItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserAsync(Guid id)
        {
            var existingItem = await usersRepository.GetUserAsync(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            await usersRepository.RemoveUserAsync(id);

            return NoContent();
        }
    }
}