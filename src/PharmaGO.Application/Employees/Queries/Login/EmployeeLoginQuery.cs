using ErrorOr;
using MediatR;
using PharmaGO.Application.Employees.Common;

namespace PharmaGO.Application.Employees.Queries.Login;

public record EmployeeLoginQuery(string Email, string Password) : IRequest<ErrorOr<EmployeeAuthenticationResult>>;