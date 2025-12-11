namespace PharmaGO.Contract.Authentication;

public record RegisterClientRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Phone,
    Guid PharmacyId
) : RegisterUserRequest(FirstName, LastName, Email, Password, Phone);