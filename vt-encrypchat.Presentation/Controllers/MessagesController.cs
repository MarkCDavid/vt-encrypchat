using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Application.Operations.Contracts.Messages;
using vt_encrypchat.Presentation.WebModels.Extensions;
using vt_encrypchat.Presentation.WebModels.Messages;

namespace vt_encrypchat.Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IGetUserMessagesOperation _getUserMessagesResponse;
        private readonly ILogger<UserController> _logger;
        private readonly ISendUserMessageOperation _sendUserMessageOperation;

        public MessagesController(
            ILogger<UserController> logger,
            IGetUserMessagesOperation getUserMessagesResponse,
            ISendUserMessageOperation sendUserMessageOperation)
        {
            _logger = logger;
            _getUserMessagesResponse = getUserMessagesResponse;
            _sendUserMessageOperation = sendUserMessageOperation;
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUserMessages(
            [FromRoute] string id,
            [FromQuery] int count = 10)
        {
            if (!RequestForAuthorizedId(id)) return Unauthorized();

            var request = new GetUserMessagesRequest
            {
                Id = id,
                Count = count
            };

            var response = await _getUserMessagesResponse.Execute(request);
            var mapped = response.MapToViewModel();

            return Ok(mapped);
        }


        [HttpPost("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUserMessages(
            [FromRoute] string id,
            [FromBody] MessageSendViewModel messageSendViewModel)
        {
            if (!RequestForAuthorizedId(id)) return Unauthorized();

            var request = new SendUserMessageRequest
            {
                Value = messageSendViewModel.Message,
                Time = DateTime.Now,
                From = id,
                To = messageSendViewModel.To
            };

            await _sendUserMessageOperation.Execute(request);
            return Ok();
        }


        private bool RequestForAuthorizedId(string id)
        {
            var authorizedId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return authorizedId != null && authorizedId.Equals(id);
        }
    }
}