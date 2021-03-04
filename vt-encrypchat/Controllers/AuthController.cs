using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Data.Contracts.Repository;
using vt_encrypchat.Data.Entity;
using vt_encrypchat.WebModels;

namespace vt_encrypchat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUserRepository _userRepository;

        public AuthController(ILogger<AuthController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpPost("signUp")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult SignUp([FromBody]SignUpViewModel signUpViewModel)
        {
            _userRepository.SaveUser(new User
            {
                DisplayName = signUpViewModel.Username,
                Username = signUpViewModel.Username,
                Password = signUpViewModel.Password
            });
            return Ok();
        }
        
        [HttpPost("signIn")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult SingIn([FromBody]SignInViewModel signInViewModel)
        {
            return Ok(signInViewModel);
        }
    }
}