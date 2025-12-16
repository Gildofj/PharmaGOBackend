using ErrorOr;
using MediatR;
using PharmaGO.Core.Interfaces.Persistence;
using PharmaGO.Core.Entities;
using PharmaGO.Core.Common.Errors;
using PharmaGO.Core.ValueObjects;

namespace PharmaGO.Application.Pharmacies.Commands.CreatePharmacy;

public class CreatePharmacyCommandHandler(IPharmacyRepository pharmacyRepository)
    : IRequestHandler<CreatePharmacyCommand, ErrorOr<Pharmacy>>
{
    public async Task<ErrorOr<Pharmacy>> Handle(CreatePharmacyCommand command, CancellationToken cancellationToken)
    {
        var addressResult = Address.Create(
            street: command.Street,
            number: command.Number,
            neighborhood: command.Neighborhood,
            city: command.City,
            state: command.State,
            country: command.Country,
            zipCode: command.ZipCode
        );

        if (addressResult.IsError)
        {
            return addressResult.Errors;
        }

        var pharmacyResult = Pharmacy.Create(
            name: command.Name,
            cnpj: command.Cnpj,
            contactNumber: command.ContactNumber,
            address: addressResult.Value
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