using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Application.Operations.Contracts.Messages;
using vt_encrypchat.Data.Contracts.Repository;
using vt_encrypchat.Domain.Entity;

namespace vt_encrypchat.Application.Operations.Messages
{
    public class GetUserMessagesOperation : IGetUserMessagesOperation
    {
        private readonly ILogger<GetUserMessagesOperation> _logger;
        private readonly IMessageRepository _messageRepository;

        public GetUserMessagesOperation(
            ILogger<GetUserMessagesOperation> logger,
            IMessageRepository messageRepository)
        {
            _logger = logger;
            _messageRepository = messageRepository;
        }

        public async Task<GetUserMessagesResponse> Execute(GetUserMessagesRequest request)
        {
            var messages = await _messageRepository.GetMessages(
                request.Sender,
                request.Recipient,
                request.Count);
            return MapToResponse(messages);
        }

        private GetUserMessagesResponse MapToResponse(IEnumerable<Message> messages)
        {
            return new()
            {
                Messages = messages.Select(MapMessage)
            };
        }

        private GetUserMessagesResponse.Message MapMessage(Message message)
        {
            return new()
            {
                FromValue = message.FromValue,
                ToValue = message.ToValue,
                Time = message.Time,
                From = MapUser(message.From),
                To = MapUser(message.To)
            };
        }

        private static GetUserMessagesResponse.User MapUser(MessageUser user)
        {
            return new()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                GpgKey = user.GpgKey
            };
        }
    }
}