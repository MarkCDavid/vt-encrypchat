using vt_encrypchat.Application.Operations.Contracts.User;
using vt_encrypchat.Presentation.WebModels.User;

namespace WebModels.Extensions
{
    public static class UserSettingsViewModelMappingExtensions
    {
        public static UserSettingsViewModel MapToViewModel(this GetUserSettingsResponse response)
        {
            return new()
            {
                DisplayName = response.DisplayName,
                GpgKey = response.GpgKey
            };
        }


    }
}