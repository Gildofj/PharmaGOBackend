using ErrorOr;
using MediatR;
using PharmaGO.Application.Clients.Common;
using PharmaGO.Application.Employees.Common;
using PharmaGO.Core.Common.Errors;
using PharmaGO.Core.Interfaces.Authentication;
using PharmaGO.Core.Interfaces.Persistence;
using BC = BCrypt.Net.BCrypt;

namespace PharmaGO.Application.Employees.Queries.Login;

public class EmployeeLoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IEmployeeRepository employeeRepository)
    : IRequestHandler<EmployeeLoginQuery, ErrorOr<EmployeeAuthenticationResult>>
{
    public async Task<ErrorOr<EmployeeAuthenticationResult>> Handle(EmployeeLoginQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (
            await employeeRepository.GetEmployeeByEmailAsync(query.Email) is not { } employee ||
            !BC.Verify(query.Password, employee.Password)
        )
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var token = jwtTokenGenerator.GenerateToken(employee);

        return new EmployeeAuthenticationResult(employee, token);
    }
}