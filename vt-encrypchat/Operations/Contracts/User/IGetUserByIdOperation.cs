using System.Threading.Tasks;

namespace vt_encrypchat.Operations.Contracts.User
{
    public interface IGetUserByIdOperation
    {
        Task<GetUserByIdResponse> Execute(GetUserByIdRequest request);
    }

    public class GetUserByIdRequest
    {
        public int Id { get; set; }
    }
    
    public class GetUserByIdResponse
    {
        public UserModel User { get; set; }

        public class UserModel
        {
            public int Id { get; set; }
            public string DisplayName { get; set; }
            public string GpgKey { get; set; }
        }
    }
}