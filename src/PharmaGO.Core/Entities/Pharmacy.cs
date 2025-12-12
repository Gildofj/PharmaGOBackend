using ErrorOr;
using PharmaGO.Core.Common.Errors;
using PharmaGO.Core.Entities.Base;

namespace PharmaGO.Core.Entities;

public class Pharmacy : Entity
{
    public string Name { get; set; } = null!;
    public string Cnpj { get; set; } = null!;
    public virtual ICollection<Product> Products { get; set; } = null!;
    public virtual ICollection<Client> Clients { get; set; } = null!;

    public static ErrorOr<Pharmacy> CreatePharmacy(
        string name,
        string cnpj
    )
    {
        var pharmacy = new Pharmacy
        {
            Name = name,
            Cnpj = cnpj
        };

        if (pharmacy.ValidatePharmacyData() is { } errors)
        {
            return errors;
        }

        return pharmacy;
    }

    public List<Error>? ValidatePharmacyData()
    {
        List<Error> errors = [];
        
        if (string.IsNullOrEmpty(Name))
            errors.Add(Errors.Pharmacy.NameNotInformed);

        if (string.IsNullOrEmpty(Cnpj))
            errors.Add(Errors.Pharmacy.CnpjNotInformed);

        return errors.Count > 0 ? errors : null;
    }
}