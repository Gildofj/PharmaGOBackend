using ErrorOr;
using MediatR;
using PharmaGOBackend.Core.Persistence;
using PharmaGOBackend.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using PharmaGOBackend.Core.Common.Errors;

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

    public static Error? ValidatePharmacyData(RegisterPharmacyCommand command)
    {
        if (string.IsNullOrEmpty(command.Name))
            return Errors.Pharmacy.NameNotInformed;

        if (string.IsNullOrEmpty(command.Cnpj))
            return Errors.Pharmacy.CnpjNotInformed;

        return null;
    }
}