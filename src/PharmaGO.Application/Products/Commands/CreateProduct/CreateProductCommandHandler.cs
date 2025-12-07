using ErrorOr;
using MediatR;
using PharmaGO.Core.Interfaces.Persistence;
using PharmaGO.Core.Entities;
using PharmaGO.Core.Common.Errors;

namespace PharmaGO.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(IProductRepository productRepository)
    : IRequestHandler<CreateProductCommand, ErrorOr<Product>>
{
    public async Task<ErrorOr<Product>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (ValidateProductData(command) is { } error)
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

        await productRepository.AddAsync(product);

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