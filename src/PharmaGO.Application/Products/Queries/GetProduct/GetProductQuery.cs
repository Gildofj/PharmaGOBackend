using ErrorOr;
using MediatR;
using PharmaGO.Core.Entities;

namespace PharmaGO.Application.Products.Queries.GetProduct;

public record GetProductQuery(Guid Id) : IRequest<ErrorOr<Product>>;