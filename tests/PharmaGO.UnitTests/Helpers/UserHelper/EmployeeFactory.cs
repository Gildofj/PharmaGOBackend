using PharmaGO.Core.Entities;

namespace PharmaGO.UnitTests.Helpers.UserHelper;

public static class EmployeeFactory
{
    public static Employee GetDefaultEmployee()
    {
        return new Employee
        {
            Id = Guid.NewGuid(),
            FirstName = "Teste",
            LastName = Common.GetRandomName(),
            Email = "teste@teste.com",
            Password = BC.HashPassword("123", BC.GenerateSalt(12)),
            PharmacyId = Guid.Empty,
        };
    }
}