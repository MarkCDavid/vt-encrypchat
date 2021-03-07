using System.Threading.Tasks;

namespace vt_encrypchat.Application.Operations.Contracts.User
{
    public interface IGetUserExistsOperation
    {
        Task<GetUserExistsResponse> Execute(GetUserExistsRequest request);
    }

    public class GetUserExistsRequest
    {
        public string Username { get; set; }
    }

    public class GetUserExistsResponse
    {
        public bool UserExists { get; set; }
    }
}