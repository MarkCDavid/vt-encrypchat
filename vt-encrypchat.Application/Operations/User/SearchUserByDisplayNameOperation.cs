using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Application.Operations.Contracts.User;
using vt_encrypchat.Data.Contracts.Repository;

namespace vt_encrypchat.Application.Operations.User
{
    public class SearchUserByDisplayNameOperation : ISearchUserByDisplayNameOperation
    {
        private readonly ILogger<SearchUserByDisplayNameOperation> _logger;
        private readonly IUserRepository _userRepository;

        public SearchUserByDisplayNameOperation(
            ILogger<SearchUserByDisplayNameOperation> logger,
            IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<SearchUserByDisplayNameResponse> Execute(SearchUserByDisplayNameRequest request)
        {
            var users = await _userRepository.GetUsers(request.Search);
            return MapToResponse(users);
        }


        private SearchUserByDisplayNameResponse MapToResponse(IEnumerable<Domain.Entity.User> users)
        {
            return new()
            {
                Users = users.Select(MapUser)
            };
        }

        private SearchUserByDisplayNameResponse.User MapUser(Domain.Entity.User user)
        {
            return new()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                GpgKey = user.GpgKeys?
                    .OrderByDescending(key => key.Date)
                    .FirstOrDefault()?.Value
            };
        }
    }
}