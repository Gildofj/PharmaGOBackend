using ErrorOr;

namespace PharmaGOBackend.Domain.Common.Errors;
public static partial class Errors
{
    public static class Client
    {
        public static Error DuplicateEmail => Error.Conflict(code: "Client.DuplicateEmail", description: "Email is alrealdy in use.");
    }
}
