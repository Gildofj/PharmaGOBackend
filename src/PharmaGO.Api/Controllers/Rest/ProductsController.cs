using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmaGO.Api.Controllers.Common;
using PharmaGO.Application.Common.Auth;
using PharmaGO.Application.Common.Auth.Constants;
using PharmaGO.Application.Products.Commands.CreateProduct;
using PharmaGO.Application.Products.Queries.GetProduct;
using PharmaGO.Contract.Product;

namespace PharmaGO.Api.Controllers.Rest;

[Route("api/[controller]")]
public class ProductsController(ISender mediator, IMapper mapper) : ApiController
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await mediator.Send(new GetProductQuery(id));

        result.Match(
            product => Ok(mapper.Map<ProductResponse>(product)),
            error => Problem(error)
        );

        return Ok();
    }

    [HttpPost("({pharmacyId:guid})")]
    [Authorize(Policy = Policies.ManageProduct)]
    public async Task<IActionResult> Post([FromRoute] Guid pharmacyId, CreateProductRequest request)
    {
        request.PharmacyId = pharmacyId;
        var command = mapper.Map<CreateProductCommand>(request);
        var result = await mediator.Send(command);

        return result.Match(
            products => Ok(mapper.Map<ProductResponse>(products)),
            errors => Problem(errors)
        );
    }
}