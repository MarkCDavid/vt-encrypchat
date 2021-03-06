using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Data.Contracts.Repository;
using vt_encrypchat.Data.Entity;
using vt_encrypchat.WebModels;
using vt_encrypchat.WebModels.Extensions;

namespace vt_encrypchat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> Users([FromQuery] string search)
        {
            IEnumerable<User> users = await _userRepository.GetUsers(search);
            IEnumerable<UserViewModel> mapped = users.MapToViewModel();
            return Ok(mapped);
        }
        
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> User([FromRoute] int id)
        {
            User user = await _userRepository.Get(id);
            UserViewModel mapped = user.MapToViewModel();
            return Ok(mapped);
        }
    }
}