using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using PharmaGO.Contract.Authentication;
using PharmaGO.IntegrationTests.Infrastructure;
using PharmaGO.IntegrationTests.Infrastructure.Fixtures;

namespace PharmaGO.IntegrationTests.Authentication.Employee;

public class LoginTests(PostgreSqlFixture dbFixture, EnviromentVarsFixture enviromentVarsFixture)
    : IntegrationTestBase(dbFixture, enviromentVarsFixture)
{
    [Fact]
    public async Task Login_WhenValidCredentials_ShouldReturnToken()
    {
        var pharmacyId = await CreateTestPharmacyAsync();
        const string email = "login@test.com";
        const string password = "Employee@123";

        await RegisterEmployeeAsync(email, password, pharmacyId);

        var loginCommand = new { Email = email, Password = password };

        var response = await HttpClient.PostAsJsonAsync("/api/auth/admin/login", loginCommand);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();
        result.Should().NotBeNull();
        result.Token.Should().NotBeNullOrEmpty();
        result.Email.Should().Be(email);
    }

    [Fact]
    public async Task Login_WhenInvalidPassword_ShouldReturnUnauthorized()
    {
        var pharmacyId = await CreateTestPharmacyAsync();
        const string email = "test@test.com";

        await RegisterEmployeeAsync(email, "CorrectPassword@123", TestPharmacy.Id);

        var loginCommand = new { Email = email, Password = "WrongPassword@123" };

        var response = await HttpClient.PostAsJsonAsync("/api/auth/admin/login", loginCommand);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task CompleteFlow_RegisterLoginAndAccess_ShouldWork()
    {
        var pharmacyId = await CreateTestPharmacyAsync();
        const string email = "flow@test.com";
        const string password = "Employee@123";

        await RegisterEmployeeAsync(email, password, TestPharmacy.Id);

        var token = await AuthenticateEmployeeAsync(email, password);
        token.Should().NotBeNullOrEmpty();

        SetAuthorizationHeader(token);
        var response = await HttpClient.GetAsync("/api/products");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    private async Task RegisterEmployeeAsync(string email, string password, Guid pharmacyId)
    {
        var token = await AuthenticateAdminAsync();
        SetAuthorizationHeader(token);

        var registerCommand = new
        {
            Email = email,
            Password = password,
            FirstName = "Test",
            LastName = "User",
            Phone = "48999999999",
        };

        var response = await HttpClient.PostAsJsonAsync($"/api/auth/admin/register?pharmacyId={pharmacyId}", registerCommand);
        response.EnsureSuccessStatusCode();
    }
}