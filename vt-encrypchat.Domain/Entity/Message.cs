using System;

namespace vt_encrypchat.Domain.Entity
{
    public class Message : BaseEntity
    {
        public string FromValue { get; set; }
        
        public string ToValue { get; set; }
        public DateTime Time { get; set; }
        public MessageUser From { get; set; }
        public MessageUser To { get; set; }
    }

    public class MessageUser
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string GpgKey { get; set; }
    }
}