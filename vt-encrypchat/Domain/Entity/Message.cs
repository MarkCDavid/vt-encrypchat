using System;

namespace vt_encrypchat.Domain.Entity
{
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