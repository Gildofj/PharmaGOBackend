using ErrorOr;
using MediatR;
using PharmaGO.Core.Entities;

namespace PharmaGO.Application.Pharmacies.Commands.CreatePharmacy;

public record CreatePharmacyCommand(
    string Name,
    string Cnpj
) : IRequest<ErrorOr<Pharmacy>>;