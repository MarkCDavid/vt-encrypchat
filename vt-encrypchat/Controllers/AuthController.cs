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
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            ILogger<AuthController> logger,
            ICheckUserCredentialValidityOperation checkUserCredentialValidityOperation,
            ICreateUserOperation createUserOperation,
            IGetUserExistsOperation getUserExistsOperation)
        {
            _logger = logger;
            _checkUserCredentialValidityOperation = checkUserCredentialValidityOperation;
            _createUserOperation = createUserOperation;
            _getUserExistsOperation = getUserExistsOperation;
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
            var request = new CheckUserCredentialValidityRequest
            {
                Username = loginViewModel.Username,
                Password = loginViewModel.Password
            };

            var response = await _checkUserCredentialValidityOperation.Execute(request);

            if (!response.Valid) return Unauthorized();

            Claim[] claims = {new(ClaimTypes.Name, loginViewModel.Username)};
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