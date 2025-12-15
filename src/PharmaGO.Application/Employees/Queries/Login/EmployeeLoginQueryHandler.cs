using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PharmaGO.Application.Common.Auth.Constants;
using PharmaGO.Application.Common.Interfaces;
using PharmaGO.Application.Employees.Common;
using PharmaGO.Core.Common.Constants;
using PharmaGO.Core.Common.Errors;
using PharmaGO.Core.Interfaces.Persistence;
using PharmaGO.Core.Interfaces.Services;

namespace PharmaGO.Application.Employees.Queries.Login;

public class EmployeeLoginQueryHandler(
    UserManager<IdentityUser<Guid>> userManager,
    IJwtTokenGenerator jwtTokenGenerator,
    IEmployeeRepository employeeRepository)
    : IRequestHandler<EmployeeLoginQuery, ErrorOr<EmployeeAuthenticationResult>>
{
    public async Task<ErrorOr<EmployeeAuthenticationResult>> Handle(EmployeeLoginQuery query,
        CancellationToken cancellationToken)
    {
        if (
            await employeeRepository.FindEmployeeByEmailAsync(query.Email) is not { } employee ||
            await userManager.FindByEmailAsync(query.Email) is not { } user ||
            !await userManager.CheckPasswordAsync(user, query.Password)
        )
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var token = await jwtTokenGenerator.GenerateToken(
            new AuthContext
            {
                Person = employee,
                User = user,
                UserType = UserType.Employee,
                PharmacyId = employee.PharmacyId
            }
        );

        return new EmployeeAuthenticationResult(employee, token);
    }
}