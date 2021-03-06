using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Data.Contracts.Repository;

namespace vt_encrypchat.Operations.Contracts.User
{
    class GetUserByIdOperation : IGetUserByIdOperation
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
            Domain.Entity.User user = await _userRepository.Get(request.Id);
            return MapToResponse(user);
        }

        private GetUserByIdResponse MapToResponse(Domain.Entity.User user)
        {
            return new()
            {
                User = new GetUserByIdResponse.UserModel()
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