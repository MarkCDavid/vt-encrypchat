using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Application.Operations.Contracts.User;
using vt_encrypchat.Data.Contracts.Repository;

namespace vt_encrypchat.Application.Operations.User
{
    public class GetUserExistsOperation : IGetUserExistsOperation
    {
        private readonly ILogger<GetUserExistsOperation> _logger;
        private readonly IUserRepository _userRepository;

        public GetUserExistsOperation(ILogger<GetUserExistsOperation> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
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