using ErrorOr;

namespace PharmaGOBackend.Core.Common.Errors;

public static partial class Errors
{
    public static class Pharmacy
    {
        public static Error NameNotInformed => Error.Conflict(code: "Pharmacy.NameNotInformed ", description: "Name not informed.");
        public static Error CnpjNotInformed => Error.Conflict(code: "Pharmacy.CnpjNotInformed", description: "CNPJ not informed.");
    }
}
