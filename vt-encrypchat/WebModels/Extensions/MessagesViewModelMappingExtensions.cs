using System.Collections.Generic;
using System.Linq;
using vt_encrypchat.Operations.Contracts.Messages;
using vt_encrypchat.Operations.Contracts.User;

namespace vt_encrypchat.WebModels.Extensions.User
{
    public static class MessagesViewModelMappingExtensions
    {
        public static IEnumerable<MessageViewModel> MapToViewModel(this GetUserMessagesResponse response)
        {
            return response.Messages.Select(MapToViewModel);
        }
        
        private static MessageViewModel MapToViewModel(this GetUserMessagesResponse.Message message)
        {
            return new()
            {
                Message = message.Value,
                Time = message.Time,
                From = message.From.MapToViewModel(),
                To = message.To.MapToViewModel()
            };
        }
        
        private static UserViewModel MapToViewModel(this GetUserMessagesResponse.User user)
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