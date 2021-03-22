using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Application.Operations.Contracts.User;
using vt_encrypchat.Application.Operations.User.Extensions;
using vt_encrypchat.Data.Contracts.Repository;

namespace vt_encrypchat.Application.Operations.User
{
    public class CheckUserCredentialValidityOperation : ICheckUserCredentialValidityOperation
    {
        private readonly ILogger<CheckUserCredentialValidityOperation> _logger;
        private readonly IUserRepository _userRepository;

        public CheckUserCredentialValidityOperation(ILogger<CheckUserCredentialValidityOperation> logger,
            IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<CheckUserCredentialValidityResponse> Execute(
            CheckUserCredentialValidityRequest request)
        {
            var user = await _userRepository.GetUserByUsername(request.Username);
            if (user == null)
            {
                return CheckUserCredentialValidityResponse.Invalid();
            }
            
            var receivedPassword = UserOperationsExtensions.Hash(request.Password, user.Salt);

            return receivedPassword.Equals(user.Password) 
                ? CheckUserCredentialValidityResponse.Ok() 
                : CheckUserCredentialValidityResponse.Invalid();
        }
    }
}