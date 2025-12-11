using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PharmaGO.Application.Employees.Common;
using PharmaGO.Core.Common.Errors;
using PharmaGO.Core.Interfaces.Persistence;
using PharmaGO.Core.Interfaces.Services;

namespace PharmaGO.Application.Employees.Queries.Login;

public class EmployeeLoginQueryHandler(
    IJwtTokenGenerator jwtTokenGenerator,
    IEmployeeRepository employeeRepository,
    IPasswordHashingService passwordHashing)
    : IRequestHandler<EmployeeLoginQuery, ErrorOr<EmployeeAuthenticationResult>>
{
    public async Task<ErrorOr<EmployeeAuthenticationResult>> Handle(EmployeeLoginQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (
            await employeeRepository.GetEmployeeByEmailAsync(query.Email) is not { } employee ||
            !passwordHashing.VerifyPasswordHash(employee, query.Password, employee.Password)
        )
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var token = jwtTokenGenerator.GenerateToken(employee);

        return new EmployeeAuthenticationResult(employee, token);
    }
}