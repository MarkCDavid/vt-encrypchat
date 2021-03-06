using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Data.Contracts.Repository;
using vt_encrypchat.Operations.Contracts.User;
using vt_encrypchat.WebModels;
using vt_encrypchat.WebModels.Extensions;
using vt_encrypchat.WebModels.Extensions.User;

namespace vt_encrypchat.Controllers
{
    
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IGetUserByIdOperation _getUserByUsernameOperation;
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
            SearchUserByDisplayNameRequest request = new SearchUserByDisplayNameRequest
            {
                DisplayName = search
            };

            SearchUserByDisplayNameResponse response = await _searchUserByDisplayNameOperation.Execute(request);
            IEnumerable<UserViewModel> mapped = response.MapToViewModel();
            return Ok(mapped);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUser([FromRoute] string id)
        {
            GetUserByIdRequest request = new GetUserByIdRequest
            {
                Id = id
            };
            
            GetUserByIdResponse response = await _getUserByUsernameOperation.Execute(request);
            UserViewModel mapped = response.MapToViewModel();
            return Ok(mapped);
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateUserSettings(
            [FromRoute] string id,
            [FromBody] UserSettingsViewModel userSettingsViewModel)
        {
            if (!RequestForAuthorizedId(id))
            {
                return Unauthorized();
            }

            UpdateUserRequest request = new UpdateUserRequest()
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
            string authorizedId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return authorizedId != null && authorizedId.Equals(id);
        }
    }
}