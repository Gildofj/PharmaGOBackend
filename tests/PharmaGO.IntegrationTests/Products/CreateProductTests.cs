using System.Net;
using System.Net.Http.Json;
using Bogus;
using FluentAssertions;
using PharmaGO.Core.Common.Constants;
using PharmaGO.IntegrationTests.Infrastructure;

namespace PharmaGO.IntegrationTests.Products;

public class CreateProductTests(PostgreSqlFixture dbFixture) : IntegrationTestBase(dbFixture)
{
    [Fact]
    public async Task CreateProduct_WhenAuthenticated_ShouldPersistToDatabase()
    {
        var pharmacyId = await CreateTestPharmacyAsync();
        
        var token = await CreateAndAuthenticateEmployeeAsync(pharmacyId);
        SetAuthorizationHeader(token);

        var createCommand = new
        {
            Name = "Aspirina",
            Price = 10.50m,
            Category = "Health",
        };

        var response = await HttpClient.PostAsJsonAsync("/api/products", createCommand);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var productId = await response.Content.ReadFromJsonAsync<Guid>();

        await using var context = Context;
        var product = await context.Products.FindAsync(productId);
        
        product.Should().NotBeNull();
        product!.Name.Should().Be("Aspirina");
        product.Price.Should().Be(10.50m);
    }

    [Fact]
    public async Task CreateProduct_WithoutAuthentication_ShouldReturnUnauthorized()
    {
        var createCommand = new
        {
            Name = "Aspirina",
            Price = 10.50m,
            Category = "Health",
        };

        var response = await HttpClient.PostAsJsonAsync("/api/products", createCommand);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    private async Task<string> CreateAndAuthenticateEmployeeAsync(Guid pharmacyId)
    {
        var email = $"employee_{Guid.NewGuid():N}@test.com";
        const string password = "Employee@123";

        var registerCommand = new
        {
            Email = email,
            Password = password,
            FirstName = "Test",
            LastName = "Employee",
            Cpf = Guid.NewGuid().ToString("N")[..11],
            Phone = "48999999999",
            PharmacyId = pharmacyId,
        };

        await HttpClient.PostAsJsonAsync("/api/auth/register/employee", registerCommand);
        
        return await AuthenticateAsync(email, password);
    }
}