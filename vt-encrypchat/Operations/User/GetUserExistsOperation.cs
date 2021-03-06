using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Data.Contracts.Repository;
using vt_encrypchat.Operations.Contracts.User;

namespace vt_encrypchat.Operations.User
{
    public class GetUserExistsOperation : IGetUserExistsOperation
    {
        private readonly ILogger<GetUserExistsOperation> _logger;
        private readonly IUserRepository _userRepository;

        public GetUserExistsOperation(IUserRepository userRepository, ILogger<GetUserExistsOperation> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<GetUserExistsResponse> Execute(GetUserExistsRequest request)
        {
            var user = await _userRepository.GetUserByUsername(request.Username);

            return new GetUserExistsResponse
            {
                UserExists = user != null
            };
        }
    }
}