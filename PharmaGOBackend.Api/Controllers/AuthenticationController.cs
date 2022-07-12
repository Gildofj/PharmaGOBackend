using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmaGOBackend.Application.Authentication.Commands.Register;
using PharmaGOBackend.Application.Authentication.Queries.Login;
using PharmaGOBackend.Contract.Authentication;

namespace PharmaGOBackend.Api.Controllers
{
    [Route("api/auth")]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _sender;

        public AuthenticationController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);
            var authResult = await _sender.Send(command);
            
            return authResult.Match(
                result => Ok(
                    new AuthenticationResponse(
                        result.Client.Id, 
                        result.Client.FirstName, 
                        result.Client.LastName, 
                        result.Client.Email, 
                        result.Token
                        )
                    ),
                errors => Problem(errors)
                );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var command = new LoginQuery(request.Email, request.Password);
            var authResult = await  _sender.Send(command);

            return authResult.Match(
                result => Ok(
                    new AuthenticationResponse(
                        result.Client.Id,
                        result.Client.FirstName,
                        result.Client.LastName,
                        result.Client.Email,
                        result.Token
                        )
                    ),
                errors => Problem(errors)
                );
        }
    }
}
