using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmaGOBackend.Application.Commands.RegisterPharmacy;
using PharmaGOBackend.Contract.Pharmacy;

namespace PharmaGOBackend.Api.Controllers;

[Route("api/[controller]")]
public class PharmaciesController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public PharmaciesController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Post(RegisterPharmacyRequest request)
    {
        var command = _mapper.Map<RegisterPharmacyCommand>(request);
        var authResult = await _mediator.Send(command);

        return authResult.Match(
            result => Ok(_mapper.Map<PharmacyResponse>(result)),
            errors => Problem(errors)
        );
    }
}