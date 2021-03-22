using System.Threading.Tasks;

namespace vt_encrypchat.Application.Operations.Contracts.User
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

        public static CheckUserCredentialValidityResponse Ok()
        {
            return new() { Valid = true };
        }
        
        public static CheckUserCredentialValidityResponse Invalid()
        {
            return new() { Valid = false };
        }
    }
}