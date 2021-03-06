using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Data.Contracts.Repository;
using vt_encrypchat.Operations.Contracts.User;
using vt_encrypchat.WebModels;
using vt_encrypchat.WebModels.Extensions;
using vt_encrypchat.WebModels.Extensions.User;

namespace vt_encrypchat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IGetUserByIdOperation _getUserByIdOperation;
        private readonly ISearchUserByDisplayNameOperation _searchUserByDisplayNameOperation;

        public UserController(
            ILogger<UserController> logger,
            IGetUserByIdOperation getUserByIdOperation,
            ISearchUserByDisplayNameOperation searchUserByDisplayNameOperation)
        {
            _logger = logger;
            _getUserByIdOperation = getUserByIdOperation;
            _searchUserByDisplayNameOperation = searchUserByDisplayNameOperation;
        }
        

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> Users([FromQuery] string search)
        {
            SearchUserByDisplayNameRequest request = new SearchUserByDisplayNameRequest
            {
                DisplayName = search
            };

            SearchUserByDisplayNameResponse response = await _searchUserByDisplayNameOperation.Execute(request);
            IEnumerable<UserViewModel> mapped = response.MapToViewModel();
            return Ok(mapped);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public new async Task<IActionResult> User([FromRoute] int id)
        {
            GetUserByIdRequest request = new GetUserByIdRequest
            {
                Id = id
            };
            
            GetUserByIdResponse response = await _getUserByIdOperation.Execute(request);
            UserViewModel mapped = response.MapToViewModel();
            return Ok(mapped);
        }
    }
}