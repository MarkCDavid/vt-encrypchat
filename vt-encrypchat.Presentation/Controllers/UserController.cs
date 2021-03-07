using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Application.Operations.Contracts.User;
using vt_encrypchat.Presentation.WebModels.Extensions;
using vt_encrypchat.Presentation.WebModels.User;

namespace vt_encrypchat.Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IGetUserByIdOperation _getUserByUsernameOperation;
        private readonly ILogger<UserController> _logger;
        private readonly ISearchUserByDisplayNameOperation _searchUserByDisplayNameOperation;
        private readonly IUpdateUserOperation _updateUserOperation;

        public UserController(
            ILogger<UserController> logger,
            IGetUserByIdOperation getUserByUsernameOperation,
            ISearchUserByDisplayNameOperation searchUserByDisplayNameOperation,
            IUpdateUserOperation updateUserOperation)
        {
            _logger = logger;
            _getUserByUsernameOperation = getUserByUsernameOperation;
            _searchUserByDisplayNameOperation = searchUserByDisplayNameOperation;
            _updateUserOperation = updateUserOperation;
        }


        [AllowAnonymous]
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetUsers([FromQuery] string search)
        {
            var request = new SearchUserByDisplayNameRequest
            {
                DisplayName = search
            };

            var response = await _searchUserByDisplayNameOperation.Execute(request);
            var mapped = response.MapToViewModel();
            return Ok(mapped);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUser([FromRoute] string id)
        {
            var request = new GetUserByIdRequest
            {
                Id = id
            };

            var response = await _getUserByUsernameOperation.Execute(request);
            var mapped = response.MapToViewModel();
            return Ok(mapped);
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateUserSettings(
            [FromRoute] string id,
            [FromBody] UserSettingsViewModel userSettingsViewModel)
        {
            if (!RequestForAuthorizedId(id)) return Unauthorized();

            var request = new UpdateUserRequest
            {
                Id = id,
                DisplayName = userSettingsViewModel.DisplayName,
                GpgKey = userSettingsViewModel.GpgKey
            };

            await _updateUserOperation.Execute(request);
            return Ok();
        }

        private bool RequestForAuthorizedId(string id)
        {
            var authorizedId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return authorizedId != null && authorizedId.Equals(id);
        }
    }
}