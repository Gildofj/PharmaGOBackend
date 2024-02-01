using ErrorOr;
using MediatR;
using PharmaGOBackend.Core.Entities;

namespace PharmaGOBackend.Application.Pharmacies.Commands.CreatePharmacy;

public record CreatePharmacyCommand(
    string Name,
    string Cnpj
) : IRequest<ErrorOr<Pharmacy>>;