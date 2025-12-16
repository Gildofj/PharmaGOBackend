using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using PharmaGO.Contract.Authentication;
using PharmaGO.IntegrationTests.Infrastructure;

namespace PharmaGO.IntegrationTests.Authentication;

public class LoginTests(PostgreSqlFixture dbFixture) : IntegrationTestBase(dbFixture)
{
    [Fact]
    public async Task Login_WhenValidCredentials_ShouldReturnToken()
    {
        var pharmacyId = await CreateTestPharmacyAsync();
        var email = "login@test.com";
        var password = "Employee@123";

        await RegisterEmployeeAsync(email, password, pharmacyId);

        var loginCommand = new { Email = email, Password = password };

        var response = await HttpClient.PostAsJsonAsync("/api/auth/login", loginCommand);

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
        var email = "test@test.com";
        
        await RegisterEmployeeAsync(email, "CorrectPassword@123", pharmacyId);

        var loginCommand = new { Email = email, Password = "WrongPassword@123" };

        var response = await HttpClient.PostAsJsonAsync("/api/auth/login", loginCommand);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task CompleteFlow_RegisterLoginAndAccess_ShouldWork()
    {
        var pharmacyId = await CreateTestPharmacyAsync();
        var email = "flow@test.com";
        var password = "Employee@123";

        await RegisterEmployeeAsync(email, password, pharmacyId);

        var token = await AuthenticateAsync(email, password);
        token.Should().NotBeNullOrEmpty();

        SetAuthorizationHeader(token);
        var response = await HttpClient.GetAsync("/api/products");
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    private async Task RegisterEmployeeAsync(string email, string password, Guid pharmacyId)
    {
        var registerCommand = new
        {
            Email = email,
            Password = password,
            FirstName = "Test",
            LastName = "User",
            Phone = "48999999999",
            PharmacyId = pharmacyId,
        };

        var response = await HttpClient.PostAsJsonAsync("/api/auth/register/employee", registerCommand);
        response.EnsureSuccessStatusCode();
    }
}