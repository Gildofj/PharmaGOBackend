namespace PharmaGOBackend.Contract.Product;

public record RegisterProductRequest(
    string Name,
    decimal Amount,
    string Description,
    string Category,
    string Image
) {
    public Guid PharmacyId { get; set; }
}