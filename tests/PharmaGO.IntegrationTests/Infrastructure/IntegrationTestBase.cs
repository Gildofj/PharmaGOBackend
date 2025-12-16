using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using PharmaGO.Contract.Authentication;
using PharmaGO.Core.Entities;
using PharmaGO.Infrastructure.Persistence;

namespace PharmaGO.IntegrationTests.Infrastructure;

public class IntegrationTestBase : IClassFixture<PostgreSqlFixture>, IAsyncLifetime
{
    protected readonly PostgreSqlFixture DbFixture;
    protected readonly HttpClient HttpClient;
    protected PharmaGOContext Context => DbFixture.CreateContext();

    protected IntegrationTestBase(PostgreSqlFixture dbFixture)
    {
        DbFixture = dbFixture;
        
        var factory = new CustomWebApplicationFactory(DbFixture.Container.GetConnectionString());
        
        HttpClient = factory.CreateClient();
    }
    
    public virtual async Task InitializeAsync()
    {
        await DbFixture.ResetDatabaseAsync();
    }
    
    public virtual Task DisposeAsync() => Task.CompletedTask;

    protected async Task<Guid> CreateTestPharmacyAsync()
    {
        await using var context = Context;
        var pharmacy = TestDataGenerator.CreatePharmacy();
        await context.Pharmacies.AddAsync(pharmacy);
        await context.SaveChangesAsync();
        return pharmacy.Id;
    }

    protected async Task<string> AuthenticateAsync(string email, string password)
    {
        var loginRequest = new { Email = email, Password = password };
        var response = await HttpClient.PostAsJsonAsync("/api/auth/login", loginRequest);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();
        return result!.Token;
    }

    protected void SetAuthorizationHeader(string token)
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}