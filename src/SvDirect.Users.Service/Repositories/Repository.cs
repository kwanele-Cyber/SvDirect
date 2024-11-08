using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SvDirect.Users.Service.Entities;

namespace SvDirect.Users.Service.Repositories
{

    public class MongoRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly IMongoCollection<T> dbCollection;
        private readonly FilterDefinitionBuilder<T> filterBuilder = new();

        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            dbCollection = database.GetCollection<T>(collectionName);
        }

        public async Task<IReadOnlyCollection<T>> GetUsersAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<T> GetUserAsync(Guid id)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateUserAsync(T user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            await dbCollection.InsertOneAsync(user);
        }

        public async Task UpdateAsync(T user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, user.Id);
            await dbCollection.ReplaceOneAsync(filter, user);
        }

        public async Task RemoveUserAsync(Guid id)
        {

            FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, id);

            await dbCollection.DeleteOneAsync(filter);
        }
    }
}