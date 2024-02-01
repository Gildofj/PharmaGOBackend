using ErrorOr;
using MediatR;
using PharmaGOBackend.Core.Interfaces.Persistence;
using PharmaGOBackend.Core.Entities;
using PharmaGOBackend.Core.Common.Errors;

namespace PharmaGOBackend.Application.Pharmacies.Commands.CreatePharmacy;

public class CreatePharmacyCommandHandler : IRequestHandler<CreatePharmacyCommand, ErrorOr<Pharmacy>>
{
    public readonly IPharmacyRepository _pharmacyRepository;
    public CreatePharmacyCommandHandler(IPharmacyRepository pharmacyRepository)
    {
        _pharmacyRepository = pharmacyRepository;
    }

    public async Task<ErrorOr<Pharmacy>> Handle(CreatePharmacyCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (ValidatePharmacyData(command) is Error error)
            return error;

        var pharmacy = new Pharmacy
        {
            Name = command.Name,
            Cnpj = command.Cnpj,
        };

        await _pharmacyRepository.AddAsync(pharmacy);

        return pharmacy;
    }

    public static Error? ValidatePharmacyData(CreatePharmacyCommand command)
    {
        if (string.IsNullOrEmpty(command.Name))
            return Errors.Pharmacy.NameNotInformed;

        if (string.IsNullOrEmpty(command.Cnpj))
            return Errors.Pharmacy.CnpjNotInformed;

        return null;
    }
}