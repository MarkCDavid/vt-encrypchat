using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Application.Operations.Contracts.User;
using vt_encrypchat.Data.Contracts.Repository;

namespace vt_encrypchat.Application.Operations.User
{
    public class GetUserByIdOperation : IGetUserByIdOperation
    {
        private readonly ILogger<GetUserByIdOperation> _logger;
        private readonly IUserRepository _userRepository;

        public GetUserByIdOperation(ILogger<GetUserByIdOperation> logger, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<GetUserByIdResponse> Execute(GetUserByIdRequest request)
        {
            var user = await _userRepository.Get(request.Id);
            return MapToResponse(user);
        }

        private static GetUserByIdResponse MapToResponse(Domain.Entity.User user)
        {
            if (user == null)
            {
                return new GetUserByIdResponse();
            }
            
            return new GetUserByIdResponse
            {
                User = new GetUserByIdResponse.UserModel
                {
                    Id = user.Id,
                    DisplayName = user.DisplayName,
                    GpgKey = user.GpgKeys?
                        .OrderByDescending(key => key.Date)
                        .FirstOrDefault()?.Value
                }
            };
        }
    }
}