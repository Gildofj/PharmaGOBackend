using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using PharmaGO.Application.Products.Commands.CreateProduct;
using PharmaGO.Application.Products.Queries.ListProducts;
using PharmaGO.Contract.Product;

namespace PharmaGO.Api.Controllers;

[Route("odata/[controller]")]
public class ProductsController(ISender mediator, IMapper mapper) : ApiController
{
    [HttpGet]
    [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new ListProductsQuery());

        return Ok(mapper.Map<List<ProductResponse>>(result));
    }

    [HttpPost]
    [EnableQuery]
    public async Task<IActionResult> Post(Guid pharmacyId, CreateProductRequest request)
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