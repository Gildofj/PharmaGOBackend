using ErrorOr;
using MediatR;
using PharmaGOBackend.Core.Entities;
using static PharmaGOBackend.Core.Common.Constants.ProductConstans;

namespace PharmaGOBackend.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    decimal Amount,
    string Description,
    Category Category,
    string Image,
    Guid PharmacyId
) : IRequest<ErrorOr<Product>>;