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
        var pharmacyResult = Pharmacy.CreatePharmacy(
            name: command.Name,
            cnpj: command.Cnpj
        );

        if (pharmacyResult.IsError)
        {
            return pharmacyResult.Errors;
        }

        var pharmacy = pharmacyResult.Value;

        await pharmacyRepository.AddAsync(pharmacy);

        return pharmacy;
    }
}