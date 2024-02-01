using static PharmaGOBackend.Core.Common.Constants.ProductConstans;

namespace PharmaGOBackend.Contract;

public record ProductResponse(
  Guid Id,
  string Name,
  decimal Amount,
  string Image,
  string Description,
  Category Cqtegory,
  Guid PharmacyId
);
