using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmaGO.Api.Controllers.Common;
using PharmaGO.Application.Clients.Commands.Register;
using PharmaGO.Application.Clients.Queries.Login;
using PharmaGO.Application.Common.Auth.Constants;
using PharmaGO.Application.Employees.Commands.Register;
using PharmaGO.Application.Employees.Queries.Login;
using PharmaGO.Contract.Authentication;

namespace PharmaGO.Api.Controllers.Rest;

[Route("api/auth")]
public class AuthenticationController(ISender mediator, IMapper mapper) : ApiController
{
    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterClientRequest request)
    {
        var command = mapper.Map<RegisterClientCommand>(request);
        var authResult = await mediator.Send(command);

        return authResult.Match(
            result => Ok(mapper.Map<AuthenticationResponse>(result)),
            errors => Problem(errors)
        );
    }

    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var command = mapper.Map<ClientLoginQuery>(request);
        var authResult = await mediator.Send(command);

        return authResult.Match(
            result => Ok(mapper.Map<AuthenticationResponse>(result)),
            errors => Problem(errors)
        );
    }

    [HttpPost("admin/Register")]
    [Authorize(Policy = Policies.AdminOnly)]
    public async Task<IActionResult> RegisterEmployee([FromQuery] Guid pharmacyId, RegisterEmployeeRequest request)
    {
        var command = mapper.Map<RegisterEmployeeCommand>(request);
        var authResult = await mediator.Send(command);

        return authResult.Match(
            result => Ok(mapper.Map<AuthenticationResponse>(result)),
            errors => Problem(errors)
        );
    }

    [HttpPost("admin/Login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginEmployee([FromQuery] Guid pharmacyId, LoginRequest request)
    {
        var command = mapper.Map<EmployeeLoginQuery>(request);
        var authResult = await mediator.Send(command);

        return authResult.Match(
            result => Ok(mapper.Map<AuthenticationResponse>(result)),
            errors => Problem(errors)
        );
    }
}