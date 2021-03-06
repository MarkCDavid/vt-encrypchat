using System.Threading.Tasks;

namespace vt_encrypchat.Services.Contracts
{
    public interface IUserService
    {
        Task<bool> UserExists(string username);
        Task CreateUser(string username, string password);
        Task<bool> ValidUserCredentials(string username, string password);
    }
}