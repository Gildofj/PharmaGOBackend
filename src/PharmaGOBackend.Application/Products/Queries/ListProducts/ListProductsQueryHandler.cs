using MediatR;
using PharmaGOBackend.Core.Entities;
using PharmaGOBackend.Core.Interfaces.Persistence;

namespace PharmaGOBackend.Application.Products.Queries.ListProducts;

public class ListProductsQueryHandler(IProductRepository productRepository)
    : IRequestHandler<ListProductsQuery, List<Product>>
{
    public async Task<List<Product>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        return (await productRepository.GetAllAsync()).ToList();
    }
}
