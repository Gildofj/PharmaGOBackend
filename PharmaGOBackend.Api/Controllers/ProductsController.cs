using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmaGOBackend.Application.Commands.RegisterProduct;
using PharmaGOBackend.Contract.Product;
using PharmaGOBackend.Domain.Entities;

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

    [HttpPost]
    public async Task<IActionResult> Post(Guid pharmacyId, RegisterProductRequest request)
    {
        request.PharmacyId = pharmacyId;
        var command =  _mapper.Map<RegisterProductCommand>(request);
        var authResult = await  _mediator.Send(command);

        return authResult.Match(
            result => Ok(_mapper.Map<Product>(result)),
            errors => Problem(errors)
            );
    }
}