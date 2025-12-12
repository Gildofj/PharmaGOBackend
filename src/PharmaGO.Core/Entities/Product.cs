using System.ComponentModel.DataAnnotations;
using ErrorOr;
using PharmaGO.Core.Common.Errors;
using PharmaGO.Core.Entities.Base;
using static PharmaGO.Core.Common.Constants.ProductConstans;

namespace PharmaGO.Core.Entities;

public class Product : Entity
{
    [Required] public string Name { get; set; } = null!;
    [Required] public decimal Amount { get; set; }
    public string Image { get; set; } = null!;
    [MaxLength(300)] public string Description { get; set; } = null!;
    [Required] public Category Category { get; set; }
    [Required] public Guid PharmacyId { get; set; } = Guid.Empty!;
    public virtual Pharmacy Pharmacy { get; set; } = null!;

    public static ErrorOr<Product> CreateProduct(
        string name,
        string image,
        decimal amount,
        string description,
        Category category,
        Guid pharmacyId
    )
    {
        var product = new Product
        {
            Name = name,
            Image = image,
            Amount = amount,
            Description = description,
            Category = category,
            PharmacyId = pharmacyId
        };

        if (product.ValidateProductData() is { } errors)
        {
            return errors;
        }

        return product;
    }

    private List<Error>? ValidateProductData()
    {
        List<Error> errors = [];

        if (string.IsNullOrEmpty(Name))
            errors.Add(Errors.Product.NameNotInformed);

        if (Amount <= 0)
            errors.Add(Errors.Product.AmountNotInformed);

        if (Description.Length > 300)
            errors.Add(Errors.Product.DescriptionExceededMaxLength);

        if (PharmacyId == Guid.Empty)
            errors.Add(Errors.Product.PharmacyIdNotInformed);

        return errors.Count > 0 ? errors : null;
    }
}