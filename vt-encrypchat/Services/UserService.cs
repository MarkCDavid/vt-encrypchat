using System.Linq;
using System.Threading.Tasks;
using vt_encrypchat.Data.Contracts.Repository;
using vt_encrypchat.Data.Entity;
using vt_encrypchat.Services.Contracts;

namespace vt_encrypchat.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> UserExists(string username)
        {
            User user = await _userRepository.GetUserByUsername(username);
            return user != null;
        }

        public async Task CreateUser(string username, string password)
        {
            User user = new User
            {
                Username = username,
                DisplayName = username,
                Password = password
            };

            await _userRepository.Create(user);
        }

        public async Task<bool> ValidUserCredentials(string username, string password)
        {
            User user = await _userRepository.GetUserByUsername(username);
            return user != null && user.Password.Equals(password);
        }
    }
}