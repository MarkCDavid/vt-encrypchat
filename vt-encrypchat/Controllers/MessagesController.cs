using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Operations.Contracts.Messages;
using vt_encrypchat.WebModels;
using vt_encrypchat.WebModels.Extensions.User;

namespace vt_encrypchat.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IGetUserMessagesOperation _getUserMessagesResponse;

        public MessagesController(
            ILogger<UserController> logger, 
            IGetUserMessagesOperation getUserMessagesResponse)
        {
            _logger = logger;
            _getUserMessagesResponse = getUserMessagesResponse;
        }


        [AllowAnonymous]
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUserMessages([FromRoute] int id, [FromQuery] int count = 10)
        {
            GetUserMessagesRequest request = new GetUserMessagesRequest
            {
                UserId = id,
                Count = count
            };

            GetUserMessagesResponse response = await _getUserMessagesResponse.Execute(request);
            IEnumerable<MessageViewModel> mapped = response.MapToViewModel();

            return Ok(mapped);
        }
    }
}