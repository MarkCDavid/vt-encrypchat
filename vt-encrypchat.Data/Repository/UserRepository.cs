using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using vt_encrypchat.Data.Contracts.MongoDB;
using vt_encrypchat.Data.Contracts.Repository;
using vt_encrypchat.Domain.Entity;

namespace vt_encrypchat.Data.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IMongoContext mongoContext) : base(mongoContext, "users")
        {
        }

        public async Task<User> GetUserByUsername(string username)
        {
            if (username == null)
                throw new ArgumentException(null, nameof(username));

            var builder = Builders<User>.Filter;
            var filter = builder.Eq(t => t.Username, username);

            return await Get(filter);
        }

        public async Task<IEnumerable<User>> GetUsers(string search = null)
        {
            if (search == null) return await GetAll(FilterDefinition<User>.Empty);

            var builder = Builders<User>.Filter;
            var displayNameFilter = builder.Regex(t => t.DisplayName, new BsonRegularExpression(search));
            var idFilter = builder.Eq(t => t.Id, search);
            var filter = builder.Or(idFilter, displayNameFilter);

            return await GetAll(filter);
        }
    }
}