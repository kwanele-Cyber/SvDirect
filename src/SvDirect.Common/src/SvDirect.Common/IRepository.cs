using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SvDirect.Common
{
    public interface IRepository<T> where T : IEntity
    {
        Task<IReadOnlyCollection<T>> GetUsersAllAsync();

        Task<IReadOnlyCollection<T>> GetUsersAllAsync(Expression<Func<T, bool>> filter);

        Task<T> GetUserAsync(Guid id);

        Task<T> GetUserAsync(Expression<Func<T, bool>> filter);

        Task CreateUserAsync(T user);

        Task UpdateAsync(T user);

        Task RemoveUserAsync(Guid id);
    }
}