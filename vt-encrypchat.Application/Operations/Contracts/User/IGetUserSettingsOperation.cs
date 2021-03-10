using System.Threading.Tasks;

namespace vt_encrypchat.Application.Operations.Contracts.User
{
    public interface IGetUserSettingsOperation
    {
        Task<GetUserSettingsResponse> Execute(GetUserSettingsRequest request);
    }

    public class GetUserSettingsRequest
    {
        public string Id { get; set; }
    }

    public class GetUserSettingsResponse
    {
        public string DisplayName { get; set; }
        public string GpgKey { get; set; }
    }
}