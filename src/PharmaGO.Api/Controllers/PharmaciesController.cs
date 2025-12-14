using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using PharmaGO.Application.Pharmacies.Commands.CreatePharmacy;
using PharmaGO.Contract.Pharmacy;

namespace PharmaGO.Api.Controllers;

[Route("odata/[controller]")]
public class PharmaciesController(ISender mediator, IMapper mapper) : ApiController
{
    [HttpPost]
    [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
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