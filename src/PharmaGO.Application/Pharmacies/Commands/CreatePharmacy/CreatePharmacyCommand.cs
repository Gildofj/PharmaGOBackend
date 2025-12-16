using ErrorOr;
using MediatR;
using PharmaGO.Core.Entities;

namespace PharmaGO.Application.Pharmacies.Commands.CreatePharmacy;

public record CreatePharmacyCommand(
    string Name,
    string Cnpj,
    string ContactNumber,
    string Street,
    string Number,
    string ZipCode,
    string City,
    string State,
    string Country,
    string Neighborhood
) : IRequest<ErrorOr<Pharmacy>>;