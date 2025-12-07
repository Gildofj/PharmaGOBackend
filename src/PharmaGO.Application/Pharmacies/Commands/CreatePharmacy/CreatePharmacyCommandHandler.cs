using ErrorOr;
using MediatR;
using PharmaGO.Core.Interfaces.Persistence;
using PharmaGO.Core.Entities;
using PharmaGO.Core.Common.Errors;

namespace PharmaGO.Application.Pharmacies.Commands.CreatePharmacy;

public class CreatePharmacyCommandHandler(IPharmacyRepository pharmacyRepository)
    : IRequestHandler<CreatePharmacyCommand, ErrorOr<Pharmacy>>
{
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

        await pharmacyRepository.AddAsync(pharmacy);

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