using Microsoft.AspNetCore.Mvc;
using PharmaGOBackend.Application.Services.Authentication;
using PharmaGOBackend.Contract.Authentication;

namespace PharmaGOBackend.Api.Controllers
{
    [Route("api/auth")]
    public class AuthenticationController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var authResult = _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);

            return authResult.Match(
                result => Ok(new AuthenticationResponse(result.Client.Id, result.Client.FirstName, result.Client.LastName, result.Client.Email, result.Token)),
                errors => Problem(errors)
                );
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var authResult = _authenticationService.Login(request.Email, request.Password);

            return authResult.Match(
                result => Ok(new AuthenticationResponse(result.Client.Id, result.Client.FirstName, result.Client.LastName, result.Client.Email, result.Token)),
                errors => Problem(errors)
                );
        }
    }
}
