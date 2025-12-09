using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmaGO.Application.Authentication.Commands.Register;
using PharmaGO.Application.Authentication.Queries.Login;
using PharmaGO.Contract.Authentication;

namespace PharmaGO.Api.Controllers;

[Route("api/auth/[action]")]
public class AuthenticationController(ISender mediator, IMapper mapper) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = mapper.Map<RegisterClientCommand>(request);
        var authResult = await mediator.Send(command);

        return authResult.Match(
            result => Ok(mapper.Map<AuthenticationResponse>(result)),
            errors => Problem(errors)
            );
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var command = mapper.Map<LoginQuery>(request);
        var authResult = await mediator.Send(command);

        return authResult.Match(
            result => Ok(mapper.Map<AuthenticationResponse>(result)),
            errors => Problem(errors)
            );
    }
}
