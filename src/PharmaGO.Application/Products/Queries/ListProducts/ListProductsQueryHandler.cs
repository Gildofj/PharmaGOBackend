using MediatR;
using PharmaGO.Core.Entities;
using PharmaGO.Core.Interfaces.Persistence;

namespace PharmaGO.Application.Products.Queries.ListProducts;

public class ListProductsQueryHandler(IProductRepository productRepository)
    : IRequestHandler<ListProductsQuery, List<Product>>
{
    public async Task<List<Product>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
    {
        return (await productRepository.GetAllAsync()).ToList();
    }
}
