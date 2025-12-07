using MediatR;
using PharmaGOBackend.Core.Entities;

namespace PharmaGOBackend.Application.Products.Queries.ListProducts;

public record ListProductsQuery : IRequest<List<Product>>;
