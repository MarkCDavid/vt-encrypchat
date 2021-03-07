using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Application.Operations.Contracts.User;
using vt_encrypchat.Application.Operations.Exceptions;
using vt_encrypchat.Data.Contracts.Repository;

namespace vt_encrypchat.Application.Operations.User
{
    public class CreateUserOperation : ICreateUserOperation
    {
        private readonly ILogger<CreateUserOperation> _logger;
        private readonly IUserRepository _userRepository;

        public CreateUserOperation(ILogger<CreateUserOperation> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task Execute(CreateUserRequest request)
        {
            var response = await _userRepository.GetUserByUsername(request.Username);
            if (response != null)
            {
                throw new OperationException(
                    $"Creating a user with username {request.Username}, when such user already exists.");
            }

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