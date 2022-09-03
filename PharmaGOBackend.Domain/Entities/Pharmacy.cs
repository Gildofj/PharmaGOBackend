namespace PharmaGOBackend.Domain.Entities;

public class Pharmacy : EntityBase
{
    public string Name { get; set; } = null!;
    public virtual ICollection<Product> Products { get; set; } = null!;
    public virtual ICollection<Client> Clients { get; set; } = null!;
}