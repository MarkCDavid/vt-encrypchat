using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace vt_encrypchat.Operations.Contracts.Messages
{
    public interface IGetUserMessagesOperation
    {
        Task<GetUserMessagesResponse> Execute(GetUserMessagesRequest request);
    }

    public class GetUserMessagesRequest
    {
        public int UserId { get; set; }
        public int Count { get; set; }
    }

    public class GetUserMessagesResponse
    {
        public IEnumerable<Message> Messages;

        public class Message
        {
            public string Value { get; set; }
            public DateTime Time { get; set; }
            public User From { get; set; }
            public User To { get; set; }
        }
        
        public class User
        {
            public int Id { get; set; }
            public string DisplayName { get; set; }
            public string GpgKey { get; set; }
        }
        
    }
}