using static PharmaGO.Core.Common.Constants.ProductConstans;

namespace PharmaGO.Contract.Product;

public record ProductResponse(
  Guid Id,
  string Name,
  decimal Amount,
  string Image,
  string Description,
  Category Category,
  Guid PharmacyId
);
