using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmaGOBackend.Application.Commands.Register;
using PharmaGOBackend.Application.Queries.Login;
using PharmaGOBackend.Contract.Authentication;

namespace PharmaGOBackend.Api.Controllers;

[Route("api/auth")]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        var authResult = await _mediator.Send(command);

        return authResult.Match(
            result => Ok(_mapper.Map<AuthenticationResponse>(result)),
            errors => Problem(errors)
            );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var command =  _mapper.Map<LoginQuery>(request);
        var authResult = await  _mediator.Send(command);

        return authResult.Match(
            result => Ok(_mapper.Map<AuthenticationResponse>(result)),
            errors => Problem(errors)
            );
    }
}
