using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Services.Contracts;
using vt_encrypchat.WebModels;

namespace vt_encrypchat.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUserService _userService;

        public AuthController(ILogger<AuthController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        
        [AllowAnonymous]
        [HttpPost("signup")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> SignUp([FromBody]SignUpViewModel signUpViewModel)
        {
            bool userExists = await _userService.UserExists(signUpViewModel.Username);
            if (userExists)
            {
                return BadRequest();
            }

            await _userService.CreateUser(signUpViewModel.Username, signUpViewModel.Password);
            
            return Ok(signUpViewModel);
        }
        
        [AllowAnonymous]
        [HttpPost("login")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel loginViewModel)
        {
            bool userCredentialsValid =
                await _userService.ValidUserCredentials(loginViewModel.Username, loginViewModel.Password);
            
            if (!userCredentialsValid)
            {
                return Unauthorized();
            }

            Claim[] claims = { new(ClaimTypes.Name, loginViewModel.Username) };
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);
            
            _logger.LogInformation($"User [{loginViewModel.Username}] logged in the system.");
            
            return Redirect("/");
        }
        
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
        
    }
}