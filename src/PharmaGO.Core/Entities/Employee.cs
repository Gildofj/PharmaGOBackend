namespace PharmaGO.Core.Entities;

public class Employee : User
{
    public Guid PharmacyId { get; set; } = Guid.Empty!;
    public virtual Pharmacy Pharmacy { get; set; } = null!;
}