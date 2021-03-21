using System.Threading.Tasks;

namespace vt_encrypchat.Application.Operations.Contracts.User
{
    public interface IUpdateUserSettingsOperation
    {
        Task Execute(UpdateUserSettingsRequest request);
    }

    public class UpdateUserSettingsRequest
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string GpgKey { get; set; }
    }
}