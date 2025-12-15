using ErrorOr;
using MediatR;
using PharmaGO.Core.Entities;
using PharmaGO.Core.Interfaces.Persistence;

namespace PharmaGO.Application.Products.Queries.GetProduct;

public class GetProductQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetProductQuery, ErrorOr<Product>>
{
    public async Task<ErrorOr<Product>> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(query.Id);

        if (product is null)
        {
            return Error.NotFound("Product not found");
        }

        return product;
    }
}