using System;
using System.Linq;
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
    [Route("api/[controller]")]
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

        [HttpGet("{from}/{to}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetConversationMessages(
            [FromRoute] string from,
            [FromRoute] string to,
            [FromQuery] int count = 10)
        {
            if (!RequestForAuthorizedId(from)) return Unauthorized();

            var request = new GetUserMessagesRequest
            {
                Sender = from,
                Recipient = to,
                Count = count
            };

            var response = await _getUserMessagesResponse.Execute(request);
            var mapped = response.MapToViewModel();

            return Ok(mapped);
        }
        
        [HttpGet("{from}/{to}/{lastMessageId}")]
        [Produces("application/json")]
        public async Task<IActionResult> NewMessages(
            [FromRoute] string from,
            [FromRoute] string to,
            [FromRoute] string lastMessageId)
        {
            if (!RequestForAuthorizedId(from)) return Unauthorized();

            var request = new GetUserMessagesRequest
            {
                Sender = from,
                Recipient = to,
                Count = 1
            };

            var response = await _getUserMessagesResponse.Execute(request);
            if (!response.Messages.Any())
            {
                return Ok(PollMessagesStatusViewModel.NoNewMesssages());
            }

            if (response.Messages.First().Id == lastMessageId)
            {
                return Ok(PollMessagesStatusViewModel.NoNewMesssages());
            } 
            
            return Ok(PollMessagesStatusViewModel.HasNewMesssages());
        }


        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> SendMessage(
            [FromBody] MessageSendViewModel model)
        {
            if (!RequestForAuthorizedId(model.Sender)) return Unauthorized();

            var request = new SendUserMessageRequest
            {
                FromValue = model.SenderValue,
                ToValue = model.RecipientValue,
                Time = DateTime.Now,
                From = model.Sender,
                To = model.Recipient
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