using System.Collections.Generic;
using System.Threading.Tasks;
using vt_encrypchat.Data.Contracts.MongoDB;
using vt_encrypchat.Data.Repository;
using vt_encrypchat.Domain.Entity;

namespace vt_encrypchat.Data.Contracts.Repository
{
    class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(IMongoContext mongoContext, string collectionName) : base(mongoContext, collectionName)
        {
        }

        public Task<IEnumerable<Message>> GetMessages(string id, int count)
        {
            throw new System.NotImplementedException();
        }
    }
}