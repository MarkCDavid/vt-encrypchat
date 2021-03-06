using System.Threading.Tasks;

namespace vt_encrypchat.Operations.Contracts.User
{
    public interface ICheckUserCredentialValidityOperation
    {
        Task<CheckUserCredentialValidityResponse> Execute(CheckUserCredentialValidityRequest request);
    }

    public class CheckUserCredentialValidityRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class CheckUserCredentialValidityResponse
    {
        public bool Valid { get; set; }
    }
}