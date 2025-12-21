namespace PharmaGO.Contract.Authentication;

public record RegisterEmployeeRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Phone,
    bool IsAdmin
) : RegisterUserRequest(FirstName, LastName, Email, Password, Phone)
{
    public Guid PharmacyId { get; set; }
}