using ErrorOr;
using MediatR;
using PharmaGO.Application.Employees.Common;

namespace PharmaGO.Application.Employees.Commands.Register;

public record RegisterEmployeeCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Phone,
    Guid PharmacyId,
    bool IsAdmin
) : IRequest<ErrorOr<EmployeeAuthenticationResult>>;
