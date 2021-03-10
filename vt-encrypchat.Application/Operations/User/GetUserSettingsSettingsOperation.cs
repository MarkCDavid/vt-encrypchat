using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Application.Operations.Contracts.User;
using vt_encrypchat.Data.Contracts.Repository;
using vt_encrypchat.Domain.Entity;

namespace vt_encrypchat.Application.Operations.User
{
    public class GetUserSettingsSettingsOperation : IGetUserSettingsOperation
    {
        private readonly ILogger<GetUserSettingsSettingsOperation> _logger;
        private readonly IUserRepository _userRepository;

        public GetUserSettingsSettingsOperation(
            ILogger<GetUserSettingsSettingsOperation> logger,
            IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<GetUserSettingsResponse> Execute(GetUserSettingsRequest request)
        {
            var user = await _userRepository.Get(request.Id);
            return MapToResponse(user);
        }

        private GetUserSettingsResponse MapToResponse(Domain.Entity.User user)
        {
            return new()
            {
                DisplayName = user.DisplayName,
                GpgKey = user.GpgKeys?
                    .OrderByDescending(key => key.Date)
                    .FirstOrDefault()?.Value
            };
        }
    }
}