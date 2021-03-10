using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Application.Operations.Contracts.User;
using vt_encrypchat.Presentation.WebModels.Extensions;
using vt_encrypchat.Presentation.WebModels.User;
using WebModels.Extensions;

namespace vt_encrypchat.Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IGetUserByIdOperation _getUserByUsernameOperation;
        private readonly ILogger<UserController> _logger;
        private readonly ISearchUserByDisplayNameOperation _searchUserByDisplayNameOperation;
        private readonly IUpdateUserSettingsOperation _updateUserSettingsOperation;
        private readonly IGetUserSettingsOperation _getUserSettingsOperation;

        public UserController(
            ILogger<UserController> logger,
            IGetUserByIdOperation getUserByUsernameOperation,
            ISearchUserByDisplayNameOperation searchUserByDisplayNameOperation,
            IUpdateUserSettingsOperation updateUserSettingsOperation,
            IGetUserSettingsOperation getUserSettingsOperation)
        {
            _logger = logger;
            _getUserByUsernameOperation = getUserByUsernameOperation;
            _searchUserByDisplayNameOperation = searchUserByDisplayNameOperation;
            _updateUserSettingsOperation = updateUserSettingsOperation;
            _getUserSettingsOperation = getUserSettingsOperation;
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

        [HttpPut("settings/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateUserSettings(
            [FromRoute] string id,
            [FromBody] UserSettingsViewModel userSettingsViewModel)
        {
            if (!RequestForAuthorizedId(id))
            {
                return Unauthorized();
            }

            var request = new UpdateUseSettingsRequest
            {
                Id = id,
                DisplayName = userSettingsViewModel.DisplayName,
                GpgKey = userSettingsViewModel.GpgKey
            };

            await _updateUserSettingsOperation.Execute(request);
            return Ok();
        }
        
        [HttpGet("settings/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUserSettings([FromRoute] string id)
        {
            if (!RequestForAuthorizedId(id))
            {
                return Unauthorized();
            }

            var request = new GetUserSettingsRequest { Id = id };
            var response = await _getUserSettingsOperation.Execute(request);
            var mapped = response.MapToViewModel();
            
            return Ok(mapped);
        }

        private bool RequestForAuthorizedId(string id)
        {
            var authorizedId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return authorizedId != null && authorizedId.Equals(id);
        }
    }
}