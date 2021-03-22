using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace vt_encrypchat.Application.Operations.Contracts.Messages
{
    public interface IGetUserMessagesOperation
    {
        Task<GetUserMessagesResponse> Execute(GetUserMessagesRequest request);
    }

    public class GetUserMessagesRequest
    {
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public int Count { get; set; }
    }

    public class GetUserMessagesResponse
    {
        public IEnumerable<Message> Messages;

        public class Message
        {
            public string FromValue { get; set; }
            public string ToValue { get; set; }
            public DateTime Time { get; set; }
            public User From { get; set; }
            public User To { get; set; }
        }

        public class User
        {
            public string Id { get; set; }
            public string DisplayName { get; set; }
            public string GpgKey { get; set; }
        }
    }
}