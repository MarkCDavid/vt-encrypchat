using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Operations.Contracts.Messages;
using vt_encrypchat.Operations.Contracts.User;
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
            if (!RequestForAuthorizedId(id))
            {
                return Unauthorized();
            }
            
            GetUserMessagesRequest request = new GetUserMessagesRequest
            {
                Id = id,
                Count = count
            };

            GetUserMessagesResponse response = await _getUserMessagesResponse.Execute(request);
            IEnumerable<MessageViewModel> mapped = response.MapToViewModel();

            return Ok(mapped);
        }


        [HttpPost("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUserMessages(
            [FromRoute] string id, 
            [FromBody] MessageSendViewModel messageSendViewModel)
        {
            if (!RequestForAuthorizedId(id))
            {
                return Unauthorized();
            }
            
            SendUserMessageRequest request = new SendUserMessageRequest
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
            string authorizedId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return authorizedId != null && authorizedId.Equals(id);
        }
    }
}