using ErrorOr;
using MediatR;
using PharmaGO.Application.Employees.Common;
using PharmaGO.Core.Common.Errors;
using PharmaGO.Core.Entities;
using BC = BCrypt.Net.BCrypt;
using PharmaGO.Core.Interfaces.Authentication;
using PharmaGO.Core.Interfaces.Persistence;

namespace PharmaGO.Application.Employees.Commands.Register;

public class RegisterEmployeeCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IEmployeeRepository employeeRepository)
    : IRequestHandler<RegisterEmployeeCommand, ErrorOr<EmployeeAuthenticationResult>>
{
    public async Task<ErrorOr<EmployeeAuthenticationResult>> Handle(
        RegisterEmployeeCommand command,
        CancellationToken cancellationToken
    )
    {
        await Task.CompletedTask;

        if (await ValidateRegisterCredentials(command) is { } error)
            return error;

        var employee = new Employee
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = BC.HashPassword(command.Password, BC.GenerateSalt(12)),
            PharmacyId = command.PharmacyId
        };

        await employeeRepository.AddAsync(employee);

        var token = jwtTokenGenerator.GenerateToken(employee);

        return new EmployeeAuthenticationResult(employee, token);
    }

    private async Task<Error?> ValidateRegisterCredentials(RegisterEmployeeCommand command)
    {
        if (string.IsNullOrEmpty(command.Email))
        {
            return Errors.Authentication.EmailNotInformed;
        }

        if (string.IsNullOrEmpty(command.FirstName))
        {
            return Errors.Authentication.FirstNameNotInformed;
        }

        if (string.IsNullOrEmpty(command.LastName))
        {
            return Errors.Authentication.LastNameNotInformed;
        }

        if (string.IsNullOrEmpty(command.Password))
        {
            return Errors.Authentication.PasswordNotInformed;
        }

        if (await employeeRepository.GetEmployeeByEmailAsync(command.Email) is not null)
        {
            return Errors.Client.DuplicateEmail;
        }

        return null;
    }
}