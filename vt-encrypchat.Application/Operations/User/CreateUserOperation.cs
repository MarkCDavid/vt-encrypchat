using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Application.Operations.Contracts.User;
using vt_encrypchat.Application.Operations.Exceptions;
using vt_encrypchat.Application.Operations.User.Extensions;
using vt_encrypchat.Application.Operations.Validations.User;
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
            CreateUserOperationValidations.ValidateRequest(request);
            
            var response = await _userRepository.GetUserByUsername(request.Username);
            if (response != null)
            {
                throw new OperationException($"User with username {request.Username} already exists.");
            }

            var salt = UserOperationsExtensions.GenerateSalt();
            var user = new Domain.Entity.User
            {
                Username = request.Username,
                DisplayName = request.Username,
                Password = UserOperationsExtensions.Hash(request.Password, salt),
                Salt = salt,
            };

            await _userRepository.Create(user);
        }
    }
}