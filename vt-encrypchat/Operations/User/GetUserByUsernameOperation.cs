using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Data.Contracts.Repository;

namespace vt_encrypchat.Operations.Contracts.User
{
    class GetUserByUsernameOperation : IGetUserByUsernameOperation
    {
   
        private readonly ILogger<GetUserByUsernameOperation> _logger;
        private readonly IUserRepository _userRepository;

        public GetUserByUsernameOperation(ILogger<GetUserByUsernameOperation> logger, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        
        public async Task<GetUserByUsernameResponse> Execute(GetUserByUsernameRequest request)
        {
            Domain.Entity.User user = await _userRepository.GetUserByUsername(request.Username);
            return MapToResponse(user);
        }

        private GetUserByUsernameResponse MapToResponse(Domain.Entity.User user)
        {
            return new()
            {
                User = new GetUserByUsernameResponse.UserModel
                {
                    Id = user.Id,
                    Username = user.Username,
                    DisplayName = user.DisplayName,
                    GpgKey = user.GpgKeys?
                        .OrderByDescending(key => key.Date)
                        .FirstOrDefault()?.Value
                }
            };
        }
    }
}