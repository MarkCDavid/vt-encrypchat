using System.Threading.Tasks;

namespace vt_encrypchat.Operations.Contracts.User
{
    public interface ICreateUserOperation
    {
        Task Execute(CreateUserRequest request);
    }

    public class CreateUserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}