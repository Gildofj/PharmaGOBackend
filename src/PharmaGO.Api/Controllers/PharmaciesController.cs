using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmaGO.Application.Pharmacies.Commands.CreatePharmacy;
using PharmaGO.Contract.Pharmacy;

namespace PharmaGO.Api.Controllers;

[Route("api/[controller]")]
public class PharmaciesController(ISender mediator, IMapper mapper) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Post(CreatePharmacyRequest request)
    {
        var command = mapper.Map<CreatePharmacyCommand>(request);
        var authResult = await mediator.Send(command);

        return authResult.Match(
            result => Ok(mapper.Map<PharmacyResponse>(result)),
            errors => Problem(errors)
        );
    }
}