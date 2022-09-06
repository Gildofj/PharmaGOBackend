using ErrorOr;
using MediatR;
using PharmaGOBackend.Application.Common.Interfaces.Persistence;
using PharmaGOBackend.Domain.Entities;

namespace PharmaGOBackend.Application.Commands.RegisterProduct;

public class RegisterProductCommandHandler : IRequestHandler<RegisterProductCommand, ErrorOr<Product>>
{
    private readonly IProductRepository _productRepository;

    public RegisterProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<Product>> Handle(RegisterProductCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var product = new Product() {
            Name = request.Name,
            Amount = request.Amount,
            Description = request.Description,
            Image = request.Image,
            Category = request.Category,
            PharmacyId = request.PharmacyId
        };

        _productRepository.Add(product);

        return product;
    }
}