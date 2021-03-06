using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Operations.Contracts.User;
using vt_encrypchat.WebModels;

namespace vt_encrypchat.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ICheckUserCredentialValidityOperation _checkUserCredentialValidityOperation;
        private readonly ICreateUserOperation _createUserOperation;
        private readonly IGetUserExistsOperation _getUserExistsOperation;
        private readonly IGetUserByUsernameOperation _getUserByUsernameOperation;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            ILogger<AuthController> logger,
            ICheckUserCredentialValidityOperation checkUserCredentialValidityOperation,
            ICreateUserOperation createUserOperation,
            IGetUserExistsOperation getUserExistsOperation,
            IGetUserByUsernameOperation getUserByUsernameOperation)
        {
            _logger = logger;
            _checkUserCredentialValidityOperation = checkUserCredentialValidityOperation;
            _createUserOperation = createUserOperation;
            _getUserExistsOperation = getUserExistsOperation;
            _getUserByUsernameOperation = getUserByUsernameOperation;
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> SignUp([FromBody] SignUpViewModel signUpViewModel)
        {
            GetUserExistsRequest userExistsRequest = new GetUserExistsRequest { Username = signUpViewModel.Username };
            GetUserExistsResponse userExistsResponse = await _getUserExistsOperation.Execute(userExistsRequest);

            if (userExistsResponse.UserExists) return BadRequest();

            CreateUserRequest createUserRequest = new CreateUserRequest
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

            if (!validityResponse.Valid) return Unauthorized();

            var getUserRequest = new GetUserByUsernameRequest
            {
                Username = loginViewModel.Username
            };

            var getUserResponse = await _getUserByUsernameOperation.Execute(getUserRequest);

            Claim[] claims =
            {
                new(ClaimTypes.Name, getUserResponse.User.Username),
                new(ClaimTypes.NameIdentifier, getUserResponse.User.Id),
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