using ErrorOr;
using MediatR;
using PharmaGOBackend.Application.Common.Interfaces.Persistence;
using PharmaGOBackend.Domain.Entities;

namespace PharmaGOBackend.Application.Commands.RegisterPharmacy;

public class RegisterPharmacyCommandHandler : IRequestHandler<RegisterPharmacyCommand, ErrorOr<Pharmacy>>
{
    public readonly IPharmacyRepository _pharmacyRepository;
    public RegisterPharmacyCommandHandler(IPharmacyRepository pharmacyRepository)
    {
        _pharmacyRepository = pharmacyRepository;
    }

    public async Task<ErrorOr<Pharmacy>> Handle(RegisterPharmacyCommand command, CancellationToken cancellationToken)
    {
            await Task.CompletedTask;

            var pharmacy = new Pharmacy
            {
                Name = command.Name,
                Cnpj = command.Cnpj,
            };

            return _pharmacyRepository.Add(pharmacy);
    }
}