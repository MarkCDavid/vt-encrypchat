using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Application.Operations.Contracts.Messages;
using vt_encrypchat.Data.Contracts.Repository;
using vt_encrypchat.Domain.Entity;

namespace vt_encrypchat.Application.Operations.Messages
{
    public class SendUserMessageOperation : ISendUserMessageOperation
    {
        private readonly ILogger<SendUserMessageOperation> _logger;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;

        public SendUserMessageOperation(
            ILogger<SendUserMessageOperation> logger,
            IUserRepository userRepository,
            IMessageRepository messageRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _messageRepository = messageRepository;
        }

        public async Task Execute(SendUserMessageRequest request)
        {
            var from = await _userRepository.Get(request.From);
            if (from == null) return;

            var to = await _userRepository.Get(request.To);
            if (to == null) return;

            var message = new Message
            {
                Value = request.Value,
                Time = request.Time,
                From = MapMessageUser(from),
                To = MapMessageUser(to)
            };

            await _messageRepository.Create(message);
        }

        private static MessageUser MapMessageUser(Domain.Entity.User user)
        {
            return new()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                GpgKey = user.GpgKeys?
                    .OrderByDescending(key => key.Date)
                    .FirstOrDefault()?.Value
            };
        }
    }
}