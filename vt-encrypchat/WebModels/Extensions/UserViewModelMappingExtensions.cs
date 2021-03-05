using System.Collections.Generic;
using System.Linq;
using vt_encrypchat.Data.Entity;

namespace vt_encrypchat.WebModels.Extensions
{
    public static class UserViewModelMappingExtensions
    {
        
        public static IEnumerable<UserViewModel> MapToViewModel(this IEnumerable<User> users)
        {
            return users.Select(MapToViewModel);
        }
        public static UserViewModel MapToViewModel(this User user)
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