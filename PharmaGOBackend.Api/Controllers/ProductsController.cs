using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmaGOBackend.Application.Products.Commands.CreateProduct;
using PharmaGOBackend.Application.Products.Queries.ListProducts;
using PharmaGOBackend.Contract.Product;
using PharmaGOBackend.Core.Entities;

namespace PharmaGOBackend.Api.Controllers;

[Route("api/[controller]")]
public class ProductsController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public ProductsController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new ListProductsQuery());

        return Ok(_mapper.Map<List<Product>>(result));
    }

    [HttpPost]
    public async Task<IActionResult> Post(Guid pharmacyId, RegisterProductRequest request)
    {
        request.PharmacyId = pharmacyId;
        var command = _mapper.Map<CreateProductCommand>(request);
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<Product>(result)),
            errors => Problem(errors)
            );
    }
}