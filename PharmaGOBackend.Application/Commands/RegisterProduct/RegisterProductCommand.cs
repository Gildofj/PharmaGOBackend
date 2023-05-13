using ErrorOr;
using MediatR;
using PharmaGOBackend.Core.Entities;
using static PharmaGOBackend.Core.Common.Constants.ProductConstans;

namespace PharmaGOBackend.Application.Commands.RegisterProduct;

public record RegisterProductCommand(
    string Name,
    decimal Amount,
    string Description,
    Category Category,
    string Image,
    Guid PharmacyId
) : IRequest<ErrorOr<Product>>;