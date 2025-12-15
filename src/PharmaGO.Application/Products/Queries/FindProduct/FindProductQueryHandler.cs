using ErrorOr;
using MediatR;
using PharmaGO.Core.Common.Errors;
using PharmaGO.Core.Entities;
using PharmaGO.Core.Interfaces.Persistence;

namespace PharmaGO.Application.Products.Queries.FindProduct;

public class FindProductQueryHandler(IProductRepository productRepository)
    : IRequestHandler<FindProductQuery, ErrorOr<Product>>
{
    public async Task<ErrorOr<Product>> Handle(FindProductQuery query, CancellationToken cancellationToken)
    {
        var product = await productRepository.FindByIdAsync(query.Id);

        if (product is null)
        {
            return Errors.Product.NotFound;
        }

        return product;
    }
}