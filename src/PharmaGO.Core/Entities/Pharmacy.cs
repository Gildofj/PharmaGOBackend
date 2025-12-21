using ErrorOr;
using PharmaGO.Core.Common.Errors;
using PharmaGO.Core.Entities.Base;
using PharmaGO.Core.ValueObjects;

namespace PharmaGO.Core.Entities;

public sealed class Pharmacy : Entity
{
    public string Name { get; init; } = null!;
    public string Cnpj { get; init; } = null!;
    public string ContactNumber { get; init; } = null!;
    public Address Address { get; init; } = null!;
        
    public ICollection<Product> Products { get; init; } = null!;
    public ICollection<Client> Clients { get; init; } = null!;

    public static ErrorOr<Pharmacy> Create(
        string name,
        string cnpj,
        string contactNumber,
        Address address
    )
    {
        var pharmacy = new Pharmacy
        {
            Id =  Guid.NewGuid(),
            Name = name,
            Cnpj = cnpj,
            ContactNumber = contactNumber,
            Address = address
        };

        if (pharmacy.ValidatePharmacyData() is { } errors)
        {
            return errors;
        }

        return pharmacy;
    }

    private List<Error>? ValidatePharmacyData()
    {
        List<Error> errors = [];
        
        if (string.IsNullOrEmpty(Name))
            errors.Add(Errors.Pharmacy.NameNotInformed);

        if (string.IsNullOrEmpty(Cnpj))
            errors.Add(Errors.Pharmacy.CnpjNotInformed);

        return errors.Count > 0 ? errors : null;
    }
}