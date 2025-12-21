namespace PharmaGO.Contract.Authentication;

public record RegisterClientRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Phone,
    string Cpf
) : RegisterUserRequest(FirstName, LastName, Email, Password, Phone);