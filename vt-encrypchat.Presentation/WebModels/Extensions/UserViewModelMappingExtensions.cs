using System.Collections.Generic;
using System.Linq;
using vt_encrypchat.Application.Operations.Contracts.User;
using vt_encrypchat.Presentation.WebModels.User;

namespace vt_encrypchat.Presentation.WebModels.Extensions
{
    public static class UserViewModelMappingExtensions
    {
        public static IEnumerable<UserViewModel> MapToViewModel(this SearchUserByDisplayNameResponse response)
        {
            return response.Users.Select(MapToViewModel);
        }

        public static UserViewModel MapToViewModel(this GetUserByIdResponse response)
        {
            return response.User.MapToViewModel();
        }

        private static UserViewModel MapToViewModel(this SearchUserByDisplayNameResponse.User user)
        {
            return new()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                GpgKey = user.GpgKey
            };
        }

        private static UserViewModel MapToViewModel(this GetUserByIdResponse.UserModel user)
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