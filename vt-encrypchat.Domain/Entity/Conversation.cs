using System;

namespace vt_encrypchat.Domain.Entity
{
    public class Conversation : BaseEntity
    {
        public ConversationSender Sender { get; set; }
        public ConversationReceiver Receiver { get; set; }
    }
    
    public class ConversationSender
    {
        public string Id { get; set; }
    }

    public class ConversationReceiver
    {
        public string Id { get; set; }
        public string GpgKey { get; set; }
    }
}