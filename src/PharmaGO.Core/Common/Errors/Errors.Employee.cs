using ErrorOr;

namespace PharmaGO.Core.Common.Errors;

public static partial class Errors
{
    public static class Employee
    {
        public static Error DuplicateEmail =>
            Error.Conflict(code: "Employee.DuplicateEmail", description: "Email is already in use.");

        public static Error PharmacyIdRequired =>
            Error.Validation(code: "Employee.PharmacyIdRequired", description: "PharmacyId is required.");

        public static Error PharmacyNotFound =>
            Error.NotFound(code: "Employee.PharmacyNotFound", description: "The specified Pharmacy does not exist.");
    }
}