using System.Threading.Tasks;

namespace vt_encrypchat.Operations.Contracts.User
{
    public interface IUpdateUserOperation
    {
        Task Execute(UpdateUserRequest request);
    }

    public class UpdateUserRequest
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string GpgKey { get; set; }
    }
}