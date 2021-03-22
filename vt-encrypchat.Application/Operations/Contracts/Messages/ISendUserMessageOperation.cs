using System;
using System.Threading.Tasks;

namespace vt_encrypchat.Application.Operations.Contracts.Messages
{
    public interface ISendUserMessageOperation
    {
        Task Execute(SendUserMessageRequest request);
    }

    public class SendUserMessageRequest
    {
        public string FromValue { get; set; }
        public string ToValue { get; set; }
        public DateTime Time { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}