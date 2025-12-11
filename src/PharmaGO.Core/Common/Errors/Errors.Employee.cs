using ErrorOr;

namespace PharmaGO.Core.Common.Errors;

public static partial class Errors
{
    public static class Employee
    {
        public static Error DuplicateEmail =>
            Error.Conflict(code: "Employee.DuplicateEmail", description: "Email is alrealdy in use.");
    }
}