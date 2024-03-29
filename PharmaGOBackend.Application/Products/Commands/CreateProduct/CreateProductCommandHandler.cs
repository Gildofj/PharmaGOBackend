using ErrorOr;
using MediatR;
using PharmaGOBackend.Core.Interfaces.Persistence;
using PharmaGOBackend.Core.Entities;
using PharmaGOBackend.Core.Common.Errors;

namespace PharmaGOBackend.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ErrorOr<Product>>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<Product>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (ValidateProductData(command) is Error error)
            return error;

        var product = new Product()
        {
            Name = command.Name,
            Amount = command.Amount,
            Description = command.Description,
            Image = command.Image,
            Category = command.Category,
            PharmacyId = command.PharmacyId
        };

        await _productRepository.AddAsync(product);

        return product;
    }

    private static Error? ValidateProductData(CreateProductCommand command)
    {
        if (string.IsNullOrEmpty(command.Name))
            return Errors.Product.NameNotInformed;

        if (command.Amount <= 0)
            return Errors.Product.AmountNotInformed;

        if (command.Description.Length > 300)
            return Errors.Product.DescriptionExceededMaxLength;

        if (command.PharmacyId == Guid.Empty)
            return Errors.Product.PharmacyIdNotInformed;

        return null;
    }
}