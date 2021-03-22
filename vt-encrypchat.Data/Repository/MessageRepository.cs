using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using vt_encrypchat.Data.Contracts.MongoDB;
using vt_encrypchat.Data.Contracts.Repository;
using vt_encrypchat.Domain.Entity;

namespace vt_encrypchat.Data.Repository
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(IMongoContext mongoContext) : base(mongoContext, "messages")
        {
        }

        public async Task<IEnumerable<Message>> GetMessages(string sender, string recipient, int count)
        {
            var cursor = await Collection
                .Find(BuildFilter(sender, recipient))
                .SortByDescending(message => message.Time)
                .Limit(count)
                .ToCursorAsync();

            return await cursor.ToListAsync();
        }

        private static FilterDefinition<Message> BuildFilter(string sender, string recipient)
        {
            var builder = Builders<Message>.Filter;
            var senderFromFilter = builder.Eq(t => t.From.Id, sender);
            var recipientToFilter = builder.Eq(t => t.To.Id, recipient);
            var senderToRecipientFilter = builder.And(senderFromFilter, recipientToFilter);
            var senderToFilter = builder.Eq(t => t.To.Id, sender);
            var recipientFromFilter = builder.Eq(t => t.From.Id, recipient);
            var recipientToSenderFilter = builder.And(senderToFilter, recipientFromFilter);
            return builder.Or(senderToRecipientFilter, recipientToSenderFilter);
        }
    }
}