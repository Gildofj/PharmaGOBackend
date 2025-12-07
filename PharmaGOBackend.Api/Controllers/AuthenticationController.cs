using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmaGOBackend.Application.Authentication.Commands.Register;
using PharmaGOBackend.Application.Authentication.Queries.Login;
using PharmaGOBackend.Contract.Authentication;

namespace PharmaGOBackend.Api.Controllers;

[Route("api/auth/[action]")]
public class AuthenticationController(ISender mediator, IMapper mapper) : ApiController
{
    /// <summary>
    /// Register client
    /// </summary>
    /// <param name="pharmacyId"></param>
    /// <param name="request"></param>
    /// <returns>Created user data</returns>
    /// <response code="200">Returns a created user data</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Register(Guid pharmacyId, RegisterRequest request)
    {
        request.PharmacyId = pharmacyId;
        var command = mapper.Map<RegisterClientCommand>(request);
        var authResult = await mediator.Send(command);

        return authResult.Match(
            result => Ok(mapper.Map<AuthenticationResponse>(result)),
            errors => Problem(errors)
            );
    }

    /// <summary>
    /// Login registered user
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Logged user with jwt token</returns>
    /// <response code="200">Returns the logged user with jwt token</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
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
