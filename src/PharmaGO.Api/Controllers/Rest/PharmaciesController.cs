using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using PharmaGO.Api.Controllers.Common;
using PharmaGO.Application.Common.Auth.Constants;
using PharmaGO.Application.Pharmacies.Commands.CreatePharmacy;
using PharmaGO.Contract.Pharmacy;

namespace PharmaGO.Api.Controllers.Rest;

[Route("api/[controller]")]
public class PharmaciesController(ISender mediator, IMapper mapper) : ApiController
{
    [ODataIgnored]
    [HttpPost]
    [Authorize(Policy = Policies.ManageEmployees)]
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