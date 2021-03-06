using System.Collections.Generic;
using System.Linq;
using vt_encrypchat.Operations.Contracts.User;

namespace vt_encrypchat.WebModels.Extensions.User
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