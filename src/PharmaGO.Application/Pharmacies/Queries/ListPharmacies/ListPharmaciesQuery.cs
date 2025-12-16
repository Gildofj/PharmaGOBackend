using MediatR;
using PharmaGO.Core.Entities;

namespace PharmaGO.Application.Pharmacies.Queries.ListPharmacies;

public record ListPharmaciesQuery : IRequest<List<Pharmacy>>;