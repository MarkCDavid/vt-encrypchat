using System.Threading.Tasks;

namespace vt_encrypchat.Application.Operations.Contracts.User
{
    public interface IGetUserByIdOperation
    {
        Task<GetUserByIdResponse> Execute(GetUserByIdRequest request);
    }

    public class GetUserByIdRequest
    {
        public string Id { get; set; }
    }

    public class GetUserByIdResponse
    {
        public UserModel User { get; set; }

        public class UserModel
        {
            public string Id { get; set; }
            public string DisplayName { get; set; }
            public string GpgKey { get; set; }
        }
    }
}