using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmaGOBackend.Application.Products.Commands.CreateProduct;
using PharmaGOBackend.Application.Products.Queries.ListProducts;
using PharmaGOBackend.Contract;
using PharmaGOBackend.Contract.Product;
using PharmaGOBackend.Core.Entities;

namespace PharmaGOBackend.Api.Controllers;

[Route("api/[controller]")]
public class ProductsController(ISender mediator, IMapper mapper) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new ListProductsQuery());

        return Ok(mapper.Map<List<ProductResponse>>(result));
    }

    [HttpPost]
    public async Task<IActionResult> Post(Guid pharmacyId, CreateProductRequest request)
    {
        request.PharmacyId = pharmacyId;
        var command = mapper.Map<CreateProductCommand>(request);
        var result = await mediator.Send(command);

        return result.Match(
            result => Ok(mapper.Map<ProductResponse>(result)),
            errors => Problem(errors)
            );
    }
}