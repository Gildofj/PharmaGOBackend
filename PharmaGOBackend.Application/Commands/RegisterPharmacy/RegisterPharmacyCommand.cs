using ErrorOr;
using MediatR;
using PharmaGOBackend.Domain.Entities;

namespace PharmaGOBackend.Application.Commands.RegisterPharmacy;

public record RegisterPharmacyCommand(
    string Name,
    string Cnpj
) : IRequest<ErrorOr<Pharmacy>>;