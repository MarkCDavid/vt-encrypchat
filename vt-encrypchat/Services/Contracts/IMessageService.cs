using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using vt_encrypchat.Domain.Entity;

namespace vt_encrypchat.Services.Contracts
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> GetMessages(int id, int count);
    }

    public class Message : BaseEntity
    {
        public string Value { get; set; }
        public DateTime Time { get; set; }
        public MessageUser From { get; set; }
        public MessageUser To { get; set; }
    }

    public class MessageUser
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public GpgKey GpgKey { get; set; }
    }
}