using System.ComponentModel.DataAnnotations;
using PharmaGO.Core.Entities.Base;
using static PharmaGO.Core.Common.Constants.ProductConstans;

namespace PharmaGO.Core.Entities;

public class Product : Entity
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public decimal Amount { get; set; }
    public string Image { get; set; } = null!;
    [MaxLength(300)]
    public string Description { get; set; } = null!;
    [Required]
    public Category Category { get; set; }
    [Required]
    public Guid PharmacyId { get; set; } = Guid.Empty!;
    public virtual Pharmacy Pharmacy { get; set; } = null!;
}