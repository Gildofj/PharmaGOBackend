using ErrorOr;

namespace PharmaGO.Core.Common.Errors;

public static partial class Errors
{
    public static class Pharmacy
    {
        public static Error NameNotInformed =>
            Error.Validation(code: "Pharmacy.NameNotInformed ", description: "Name not informed.");

        public static Error CnpjNotInformed =>
            Error.Validation(code: "Pharmacy.CnpjNotInformed", description: "CNPJ not informed.");
    }
}