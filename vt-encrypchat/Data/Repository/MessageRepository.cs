using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using vt_encrypchat.Data.Contracts.MongoDB;
using vt_encrypchat.Data.Repository;
using vt_encrypchat.Domain.Entity;

namespace vt_encrypchat.Data.Contracts.Repository
{
    class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(IMongoContext mongoContext) : base(mongoContext, "messages")
        {
        }

        public async Task<IEnumerable<Message>> GetMessages(string id, int count)
        {
            var builder = Builders<Message>.Filter;
            var filter = builder
                .Eq(t => t.To.Id, id);

            var cursor = await Collection
                .Find(filter)
                .SortByDescending(message => message.Time)
                .Limit(count)
                .ToCursorAsync();

            return await cursor.ToListAsync();
        }
    }
}