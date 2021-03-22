using System.Collections.Generic;
using System.Threading.Tasks;
using vt_encrypchat.Domain.Entity;

namespace vt_encrypchat.Data.Contracts.Repository
{
    public interface IMessageRepository : IBaseRepository<Message>
    {
        Task<IEnumerable<Message>> GetMessages(string sender, string recipient, int count);
    }
}