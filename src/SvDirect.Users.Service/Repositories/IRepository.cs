using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SvDirect.Users.Service.Entities;

namespace SvDirect.Users.Service.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        Task<IReadOnlyCollection<T>> GetUsersAllAsync();

        Task<T> GetUserAsync(Guid id);

        Task CreateUserAsync(T user);

        Task UpdateAsync(T user);

        Task RemoveUserAsync(Guid id);
    }
}