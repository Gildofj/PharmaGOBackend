using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
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
    IPharmacyRepository pharmacyRepository,
    IUnitOfWork unitOfWork,
    ILogger<RegisterEmployeeCommandHandler> logger
)
    : IRequestHandler<RegisterEmployeeCommand, ErrorOr<EmployeeAuthenticationResult>>
{
    public async Task<ErrorOr<EmployeeAuthenticationResult>> Handle(
        RegisterEmployeeCommand command,
        CancellationToken cancellationToken
    )
    {
        //TODO: Usuário de cliente e de employee podem ter acessos diferentes
        if (await userManager.FindByEmailAsync(command.Email) is not null)
        {
            logger.LogWarning("User with email {Email} was already registered", command.Email);
            return Errors.Employee.DuplicateEmail;
        }

        if (await pharmacyRepository.FindByIdAsync(command.PharmacyId) is null)
        {
            logger.LogWarning("Pharmacy with id {PharmacyId} was not found", command.PharmacyId);
            return Errors.Employee.PharmacyNotFound;
        }

        var employeeResult = Employee.Create(
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
        IdentityUser<Guid>? identityUser = null;

        try
        {
            var identityUserResult = await CreateEmployeeIdentityUser(command, employee.Id);

            if (identityUserResult.IsError)
            {
                return identityUserResult.Errors;
            }

            identityUser = identityUserResult.Value;

            await employeeRepository.AddAsync(employee);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            var token = await jwtTokenGenerator.GenerateToken(
                new AuthContext
                {
                    Person = employee,
                    User = identityUser,
                    UserType = UserType.Employee,
                }
            );

            logger.LogInformation(
                "Employee {EmployeeId} registered successfully!",
                employee.Id
            );

            return new EmployeeAuthenticationResult(employee, token);
        }
        catch (Exception e)
        {
            logger.LogError(
                e,
                "Unexpected error occured while registering employee with email {Email}",
                command.Email
            );

            if (identityUser != null)
            {
                try
                {
                    await userManager.DeleteAsync(identityUser);
                    logger.LogInformation(
                        "Rolled back identity user {UserId} after registration failure",
                        identityUser.Id
                    );
                }
                catch (Exception deleteEx)
                {
                    logger.LogError(deleteEx, "Failed to rollback identity user {UserId}", identityUser.Id);
                }
            }

            return Error.Unexpected("Employee.Unexpected", e.Message);
        }
    }

    private async Task<ErrorOr<IdentityUser<Guid>>> CreateEmployeeIdentityUser(
        RegisterEmployeeCommand command,
        Guid employeeId
    )
    {
        var identityUser = new IdentityUser<Guid>
        {
            Id = employeeId,
            UserName = command.Email,
            Email = command.Email,
            PhoneNumber = command.Phone,
            EmailConfirmed = true
        };

        var createUserResult = await userManager.CreateAsync(identityUser, command.Password);
        if (!createUserResult.Succeeded)
        {
            logger.LogError(
                "Failed to create user to client {ClientId} for email {Email}. Errors: {Errors}",
                identityUser.Id,
                command.Email,
                string.Join(", ", createUserResult.Errors.Select(e => e.Description))
            );

            return createUserResult.Errors
                .Select(e => Error.Validation(e.Code, e.Description))
                .ToList();
        }

        var roleName = command.IsAdmin ? EmployeeRoles.Admin : EmployeeRoles.Employee;
        var addRoleResult = await userManager.AddToRoleAsync(identityUser, roleName);

        if (!addRoleResult.Succeeded)
        {
            logger.LogError(
                "Failed to add to role {RoleName} for client {ClientId}. Errors: {Errors}",
                roleName,
                identityUser.Id,
                string.Join(", ", addRoleResult.Errors.Select(e => e.Description))
            );

            return addRoleResult.Errors
                .Select(e => Error.Validation(e.Code, e.Description))
                .ToList();
        }

        return identityUser;
    }
}