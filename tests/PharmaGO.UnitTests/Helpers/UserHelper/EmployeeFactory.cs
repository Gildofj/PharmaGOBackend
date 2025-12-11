using PharmaGO.Core.Entities;
using PharmaGO.Core.Interfaces.Services;
using PharmaGO.Infrastructure.Services;

namespace PharmaGO.UnitTests.Helpers.UserHelper;

public static class EmployeeFactory
{
    private static readonly IPasswordHashingService PasswordHashing = new PasswordHashingService();

    public static Employee GetDefaultEmployee()
    {
        var employee = new Employee
        {
            Id = Guid.NewGuid(),
            FirstName = "Teste",
            LastName = Common.GetRandomName(),
            Email = "teste@teste.com",
            PharmacyId = Guid.NewGuid(),
        };

        employee.UpdatePassword(PasswordHashing.HashPassword(employee, "123"));

        return employee;
    }
}