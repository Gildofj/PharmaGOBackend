using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PharmaGO.Application.Employees.Common;
using PharmaGO.Core.Common.Errors;
using PharmaGO.Core.Entities;
using PharmaGO.Core.Interfaces.Services;
using PharmaGO.Core.Interfaces.Persistence;

namespace PharmaGO.Application.Employees.Commands.Register;

public class RegisterEmployeeCommandHandler(
    IJwtTokenGenerator jwtTokenGenerator,
    IEmployeeRepository employeeRepository,
    IPharmacyRepository pharmacyRepository,
    IPasswordHashingService passwordHashing)
    : IRequestHandler<RegisterEmployeeCommand, ErrorOr<EmployeeAuthenticationResult>>
{
    public async Task<ErrorOr<EmployeeAuthenticationResult>> Handle(
        RegisterEmployeeCommand command,
        CancellationToken cancellationToken
    )
    {
        if (await employeeRepository.GetEmployeeByEmailAsync(command.Email) is not null)
        {
            return Errors.Employee.DuplicateEmail;
        }

        if (await pharmacyRepository.GetByIdAsync(command.PharmacyId) is null)
        {
            return Errors.Employee.PharmacyNotFound;
        }
        
        if (string.IsNullOrEmpty(command.Password))
        {
            return Errors.Authentication.PasswordNotInformed;
        }

        var employeeResult = Employee.CreateEmployee(
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

        employee.UpdatePassword(passwordHashing.HashPassword(employee, command.Password));

        await employeeRepository.AddAsync(employee);

        var token = jwtTokenGenerator.GenerateToken(employee);

        return new EmployeeAuthenticationResult(employee, token);
    }
}