using ErrorOr;

namespace PharmaGOBackend.Core.Common.Errors;
public static partial class Errors
{
    public static class Product
    {
        public static Error NameNotInformed => Error.Conflict(code: "Product.NameNotInformed", description: "Name not informed.");
        public static Error AmountNotInformed => Error.Conflict(code: "Product.AmountNotInformed", description: "Amount not informed.");
        public static Error DescriptionExceededMaxLength => Error.Conflict(code: "Product.DescriptionExceededMaxLength", description: "Exceeded maximum description length. (Max: 300)");
        public static Error PharmacyIdNotInformed => Error.Conflict(code: "Product.PharmacyIdNotInformed", description: "PharmacyId not informed.");
    }
}
