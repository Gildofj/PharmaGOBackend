using PharmaGOBackend.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace PharmaGOBackend.Core.Entities;

public class Product : Entity
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public decimal Amount { get; set; }
    public string Image { get; set; } = null!;
    [MaxLength(300)]
    public string Description { get; set; } = null!;
    public string Category { get; set; } = null!;
    public Guid PharmacyId { get; set; } = Guid.Empty!;
    public virtual Pharmacy Pharmacy { get; set; } = null!;
}