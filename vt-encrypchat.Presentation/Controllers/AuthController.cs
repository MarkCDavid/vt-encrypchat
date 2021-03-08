using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Application.Operations.Contracts.User;
using vt_encrypchat.Application.Operations.Exceptions;
using vt_encrypchat.Presentation.WebModels;
using vt_encrypchat.Presentation.WebModels.Auth;

namespace vt_encrypchat.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly ICheckUserCredentialValidityOperation _checkUserCredentialValidityOperation;
        private readonly ICreateUserOperation _createUserOperation;
        private readonly IGetUserByUsernameOperation _getUserByUsernameOperation;

        public AuthController(
            ILogger<AuthController> logger,
            ICheckUserCredentialValidityOperation checkUserCredentialValidityOperation,
            ICreateUserOperation createUserOperation,
            IGetUserByUsernameOperation getUserByUsernameOperation)
        {
            _logger = logger;
            _checkUserCredentialValidityOperation = checkUserCredentialValidityOperation;
            _createUserOperation = createUserOperation;
            _getUserByUsernameOperation = getUserByUsernameOperation;
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> SignUp([FromBody] SignUpViewModel signUpViewModel)
        {
            var createUserRequest = new CreateUserRequest
            {
                Username = signUpViewModel.Username,
                Password = signUpViewModel.Password
            };

            await _createUserOperation.Execute(createUserRequest);

            _logger.LogInformation($"User [{signUpViewModel.Username}] created an account in the system.");

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            var validityRequest = new CheckUserCredentialValidityRequest
            {
                Username = loginViewModel.Username,
                Password = loginViewModel.Password
            };

            var validityResponse = await _checkUserCredentialValidityOperation.Execute(validityRequest);

            if (!validityResponse.Valid)
            {
                return Unauthorized(new ErrorViewModel { Error = "Some Generic Error, KEK"});
            }

            var getUserRequest = new GetUserByUsernameRequest
            {
                Username = loginViewModel.Username
            };

            var getUserResponse = await _getUserByUsernameOperation.Execute(getUserRequest);

            Claim[] claims =
            {
                new(ClaimTypes.Name, getUserResponse.User.Username),
                new(ClaimTypes.NameIdentifier, getUserResponse.User.Id)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);

            _logger.LogInformation($"User [{loginViewModel.Username}] logged in the system.");

            return Ok();
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}