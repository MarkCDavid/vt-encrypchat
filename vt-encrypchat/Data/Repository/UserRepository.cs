using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using vt_encrypchat.Data.Contracts.MongoDB;
using vt_encrypchat.Data.Contracts.Repository;
using vt_encrypchat.Data.Entity;

namespace vt_encrypchat.Data.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IMongoContext mongoContext) : base(mongoContext, "users")
        {
            
        }

        public async Task<IEnumerable<User>> GetUsers(string name = null)
        {
            FilterDefinition<User> filter = FilterDefinition<User>.Empty;
            if (name != null)
            {
                FilterDefinitionBuilder<User> builder = Builders<User>.Filter;
                filter = builder.Eq(t => t.DisplayName, name);
            }

            return await GetAll(filter);
        }
    }
}