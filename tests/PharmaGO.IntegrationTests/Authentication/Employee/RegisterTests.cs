using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PharmaGO.IntegrationTests.Infrastructure;
using PharmaGO.IntegrationTests.Infrastructure.Fixtures;

namespace PharmaGO.IntegrationTests.Authentication.Employee;

public class RegisterTests(PostgreSqlFixture dbFixture, EnviromentVarsFixture enviromentVarsFixture)
    : IntegrationTestBase(dbFixture, enviromentVarsFixture)
{
    [Fact]
    public async Task Register_WhenValidEmployee_ShouldCreateUserAndEmployee()
    {
        var pharmacyId = await CreateTestPharmacyAsync();

        var registerCommand = new
        {
            Email = "employee@test.com",
            Password = "Employee@123",
            ConfirmPassword = "Employee@123",
            FirstName = "João",
            LastName = "Silva",
            Cpf = "12345678900",
            Phone = "48999999999",
            PharmacyId = pharmacyId,
        };

        var response = await HttpClient.PostAsJsonAsync("/api/auth/admin/register", registerCommand);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var context = Context;

        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == registerCommand.Email);
        user.Should().NotBeNull();

        var employee = await context.Employees.FirstOrDefaultAsync(u => u.Email == registerCommand.Email);
        employee.Should().NotBeNull();
        employee.Id.Should().Be(user.Id);
    }

    [Fact]
    public async Task Register_WhenInvalidEmail_ShouldReturnBadRequest()
    {
        var pharmacyId = await CreateTestPharmacyAsync();

        var registerCommand = new
        {
            Email = "duplicate@test.com",
            Password = "Employee@123",
            FirstName = "Test",
            LastName = "User",
            Phone = "48999999999",
            PharmacyId = pharmacyId,
            Position = "Vendedor",
        };

        await HttpClient.PostAsJsonAsync("/api/auth/admin/register", registerCommand);

        var secondCommand = registerCommand with { Phone = "48777777777" };

        var response = await HttpClient.PostAsJsonAsync("/api/auth/admin/register", secondCommand);

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }
}