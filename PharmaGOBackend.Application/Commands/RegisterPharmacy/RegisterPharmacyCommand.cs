using ErrorOr;
using MediatR;
using PharmaGOBackend.Core.Entities;

namespace PharmaGOBackend.Application.Commands.RegisterPharmacy;

public record RegisterPharmacyCommand(
    string Name,
    string Cnpj
) : IRequest<ErrorOr<Pharmacy>>;