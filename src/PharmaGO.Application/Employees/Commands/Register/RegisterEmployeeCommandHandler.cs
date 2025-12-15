using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PharmaGO.Application.Common.Auth.Constants;
using PharmaGO.Application.Common.Interfaces;
using PharmaGO.Application.Employees.Common;
using PharmaGO.Core.Common.Constants;
using PharmaGO.Core.Common.Errors;
using PharmaGO.Core.Entities;
using PharmaGO.Core.Interfaces.Persistence;

namespace PharmaGO.Application.Employees.Commands.Register;

public class RegisterEmployeeCommandHandler(
    UserManager<IdentityUser<Guid>> userManager,
    IJwtTokenGenerator jwtTokenGenerator,
    IEmployeeRepository employeeRepository,
    IPharmacyRepository pharmacyRepository
)
    : IRequestHandler<RegisterEmployeeCommand, ErrorOr<EmployeeAuthenticationResult>>
{
    public async Task<ErrorOr<EmployeeAuthenticationResult>> Handle(
        RegisterEmployeeCommand command,
        CancellationToken cancellationToken
    )
    {
        if (await userManager.FindByEmailAsync(command.Email) is not null)
        {
            return Errors.Employee.DuplicateEmail;
        }

        if (await pharmacyRepository.FindByIdAsync(command.PharmacyId) is null)
        {
            return Errors.Employee.PharmacyNotFound;
        }

        var identityUser = new IdentityUser<Guid>
        {
            UserName = command.Email,
            Email = command.Email,
            PhoneNumber = command.Phone,
            EmailConfirmed = true
        };

        var createUserResult = await userManager.CreateAsync(identityUser, command.Password);

        if (!createUserResult.Succeeded)
        {
            return createUserResult.Errors
                .Select(e => Error.Validation(e.Code, e.Description))
                .ToList();
        }

        var roleName = command.IsAdmin ? EmployeeRoles.Admin : EmployeeRoles.Employee;
        await userManager.AddToRoleAsync(identityUser, roleName);

        var employeeResult = Employee.CreateEmployee(
            identityUserId: identityUser.Id,
            firstName: command.FirstName,
            lastName: command.LastName,
            email: command.Email,
            phone: command.Phone,
            pharmacyId: command.PharmacyId
        );

        if (employeeResult.IsError)
        {
            return employeeResult.Errors;
        }

        var employee = employeeResult.Value;

        await employeeRepository.AddAsync(employee);

        var token = await jwtTokenGenerator.GenerateToken(
            new AuthContext
            {
                Person = employee,
                User = identityUser,
                UserType = UserType.Employee,
            }
        );

        return new EmployeeAuthenticationResult(employee, token);
    }
}