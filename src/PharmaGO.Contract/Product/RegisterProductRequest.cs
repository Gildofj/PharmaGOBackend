namespace PharmaGO.Contract.Product;

public record CreateProductRequest(
    string Name,
    decimal Amount,
    string Description,
    string Category,
    string Image
)
{
    public Guid PharmacyId { get; set; }
}