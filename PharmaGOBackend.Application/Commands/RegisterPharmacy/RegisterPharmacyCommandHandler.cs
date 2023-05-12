using ErrorOr;
using MediatR;
using PharmaGOBackend.Core.Persistence;
using PharmaGOBackend.Core.Entities;

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

            return await _pharmacyRepository.AddAsync(pharmacy);
    }
}