using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace vt_encrypchat.Operations.Contracts.Messages
{
    public interface ISendUserMessageOperation
    {
        Task Execute(SendUserMessageRequest request);
    }

    public class SendUserMessageRequest
    {
        public string Value { get; set; }
        public DateTime Time { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}