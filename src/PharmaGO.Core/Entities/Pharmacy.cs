using PharmaGO.Core.Entities.Base;

namespace PharmaGO.Core.Entities;

public class Pharmacy : Entity
{
    public string Name { get; set; } = null!;
    public string Cnpj { get; set; } = null!;
    public virtual ICollection<Product> Products { get; set; } = null!;
    public virtual ICollection<Client> Clients { get; set; } = null!;
}