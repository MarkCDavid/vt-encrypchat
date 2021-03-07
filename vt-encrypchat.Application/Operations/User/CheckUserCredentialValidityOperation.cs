using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Application.Operations.Contracts.User;
using vt_encrypchat.Data.Contracts.Repository;

namespace vt_encrypchat.Application.Operations.User
{
    public class CheckUserCredentialValidityOperation : ICheckUserCredentialValidityOperation
    {
        private readonly ILogger<GetUserExistsOperation> _logger;
        private readonly IUserRepository _userRepository;

        public CheckUserCredentialValidityOperation(ILogger<GetUserExistsOperation> logger,
            IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<CheckUserCredentialValidityResponse> Execute(
            CheckUserCredentialValidityRequest request)
        {
            var user = await _userRepository.GetUserByUsername(request.Username);
            return new CheckUserCredentialValidityResponse
            {
                Valid = user != null && user.Password.Equals(request.Password)
            };
        }
    }
}