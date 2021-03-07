using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Application.Operations.Contracts.User;
using vt_encrypchat.Data.Contracts.Repository;

namespace vt_encrypchat.Application.Operations.User
{
    public class GetUserByUsernameOperation : IGetUserByUsernameOperation
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
            var user = await _userRepository.GetUserByUsername(request.Username);
            return MapToResponse(user);
        }

        private static GetUserByUsernameResponse MapToResponse(Domain.Entity.User user)
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