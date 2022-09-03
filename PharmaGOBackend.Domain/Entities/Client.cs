namespace PharmaGOBackend.Domain.Entities;
public class Client : EntityBase
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public Guid PharmacyId { get; set; } = Guid.Empty!;
    public virtual Pharmacy Pharmacy { get; set; } = null!;
}
