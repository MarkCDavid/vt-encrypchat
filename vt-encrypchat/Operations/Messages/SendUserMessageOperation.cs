using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Data.Contracts.Repository;
using vt_encrypchat.Domain.Entity;

namespace vt_encrypchat.Operations.Contracts.Messages
{
    class SendUserMessageOperation : ISendUserMessageOperation
    {
        private readonly ILogger<SendUserMessageOperation> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;

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
            Domain.Entity.User from = await _userRepository.Get(request.From);
            if (from == null)
            {
                return;
            }
            
            Domain.Entity.User to = await _userRepository.Get(request.To);
            if (to == null)
            {
                return;
            }

            Message message = new Message
            {
                Value = request.Value,
                Time = request.Time,
                From = MapMessageUser(from),
                To = MapMessageUser(to)
            };
            
            await _messageRepository.Create(message);
        }

        public MessageUser MapMessageUser(Domain.Entity.User user)
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