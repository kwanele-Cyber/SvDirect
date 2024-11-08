using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using SvDirect.Users.Service.Entities;

namespace SvDirect.Users.Service.Repositories
{
    public class UsersRepository
    {
        private const string collectionName = "items";
        private readonly IMongoCollection<User> dbCollection;
        private readonly FilterDefinitionBuilder<User> filterBuilder;

        public UsersRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("Users");
            dbCollection = database.GetCollection<User>(collectionName);
        }

        public async Task<IReadOnlyCollection<User>> GetUsersAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            FilterDefinition<User> filter = filterBuilder.Eq(entity => entity.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.CreatedAt = DateTimeOffset.UtcNow;
            user.UpdateAt = DateTimeOffset.UtcNow;

            await dbCollection.InsertOneAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            FilterDefinition<User> filter = filterBuilder.Eq(entity => entity.Id, user.Id);
            user.UpdateAt = DateTimeOffset.UtcNow;
            await dbCollection.ReplaceOneAsync(filter, user);
        }

        public async Task RemoveUserAsync(Guid id)
        {

            FilterDefinition<User> filter = filterBuilder.Eq(entity => entity.Id, id);

            await dbCollection.DeleteOneAsync(filter);
        }
    }
}