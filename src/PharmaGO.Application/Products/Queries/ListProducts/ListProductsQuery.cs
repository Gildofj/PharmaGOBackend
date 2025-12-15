using MediatR;
using PharmaGO.Core.Entities;

namespace PharmaGO.Application.Products.Queries.ListProducts;

public record ListProductsQuery() : IRequest<List<Product>>;
