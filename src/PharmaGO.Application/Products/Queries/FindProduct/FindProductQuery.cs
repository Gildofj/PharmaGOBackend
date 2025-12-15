using ErrorOr;
using MediatR;
using PharmaGO.Core.Entities;

namespace PharmaGO.Application.Products.Queries.FindProduct;

public record FindProductQuery(Guid Id) : IRequest<ErrorOr<Product>>;