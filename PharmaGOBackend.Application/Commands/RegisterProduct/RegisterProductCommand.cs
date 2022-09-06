using ErrorOr;
using MediatR;
using PharmaGOBackend.Domain.Entities;

namespace PharmaGOBackend.Application.Commands.RegisterProduct;

public record RegisterProductCommand(
    string Name,
    decimal Amount,
    string Description,
    string Category,
    string Image,
    Guid PharmacyId
) : IRequest<ErrorOr<Product>>;