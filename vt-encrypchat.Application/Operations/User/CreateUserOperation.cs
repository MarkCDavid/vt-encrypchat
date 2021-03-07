using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Application.Operations.Contracts.User;
using vt_encrypchat.Data.Contracts.Repository;

namespace vt_encrypchat.Application.Operations.User
{
    public class CreateUserOperation : ICreateUserOperation
    {
        private readonly ILogger<GetUserExistsOperation> _logger;
        private readonly IUserRepository _userRepository;

        public CreateUserOperation(IUserRepository userRepository, ILogger<GetUserExistsOperation> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task Execute(CreateUserRequest request)
        {
            var user = new Domain.Entity.User
            {
                Username = request.Username,
                DisplayName = request.Username,
                Password = request.Password
            };

            await _userRepository.Create(user);
        }
    }
}