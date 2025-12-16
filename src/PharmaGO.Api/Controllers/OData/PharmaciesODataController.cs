using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using PharmaGO.Api.Controllers.Common;
using PharmaGO.Application.Pharmacies.Queries.ListPharmacies;
using PharmaGO.Contract.Pharmacy;

namespace PharmaGO.Api.Controllers.OData;

[Route("odata/Pharmacies")]
public class PharmaciesODataController(ISender mediator, IMapper mapper) : ApiController
{
    [HttpGet]
    [EnableQuery]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new ListPharmaciesQuery());

        return Ok(mapper.Map<List<PharmacyResponse>>(result));
    }
}