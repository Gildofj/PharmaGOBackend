using MediatR;
using PharmaGOBackend.Core.Entities;
using PharmaGOBackend.Core.Interfaces.Persistence;

namespace PharmaGOBackend.Application.Products.Queries.ListProducts;

public class ListProductsQueryHandler : IRequestHandler<ListProductsQuery, List<Product>>
{
    private readonly IProductRepository _productRepository;

    public ListProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<Product>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        return (await _productRepository.GetAllAsync()).ToList();
    }
}
