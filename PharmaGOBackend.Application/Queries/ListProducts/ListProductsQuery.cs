using MediatR;
using PharmaGOBackend.Core.Entities;

namespace PharmaGOBackend.Application.Queries.ListProducts;

public record ListProductsQuery : IRequest<List<Product>>;
