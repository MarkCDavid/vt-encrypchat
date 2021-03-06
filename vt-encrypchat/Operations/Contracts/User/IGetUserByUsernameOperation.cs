using System.Threading.Tasks;

namespace vt_encrypchat.Operations.Contracts.User
{
    public interface IGetUserByUsernameOperation
    {
        Task<GetUserByUsernameResponse> Execute(GetUserByUsernameRequest request);
    }

    public class GetUserByUsernameRequest
    {
        public string Username { get; set; }
    }
    
    public class GetUserByUsernameResponse
    {
        public UserModel User { get; set; }

        public class UserModel
        {
            public string Id { get; set; }
            public string Username { get; set; }
            public string DisplayName { get; set; }
            public string GpgKey { get; set; }
        }
    }
}