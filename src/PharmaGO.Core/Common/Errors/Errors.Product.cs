using ErrorOr;

namespace PharmaGO.Core.Common.Errors;

public static partial class Errors
{
    public static class Product
    {
        public static Error NameNotInformed =>
            Error.Validation(code: "Product.NameNotInformed", description: "Name not informed.");

        public static Error AmountNotInformed =>
            Error.Validation(code: "Product.AmountNotInformed", description: "Amount not informed.");

        public static Error DescriptionExceededMaxLength => Error.Conflict(code: "Product.DescriptionExceededMaxLength",
            description: "Exceeded maximum description length. (Max: 300)");

        public static Error PharmacyIdNotInformed => Error.Validation(code: "Product.PharmacyIdNotInformed",
            description: "PharmacyId not informed.");
    }
}