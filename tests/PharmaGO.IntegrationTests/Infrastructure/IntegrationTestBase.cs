using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PharmaGO.Contract.Authentication;
using PharmaGO.Core.Entities;
using PharmaGO.Infrastructure.Persistence;
using PharmaGO.IntegrationTests.Infrastructure.Factories;
using PharmaGO.IntegrationTests.Infrastructure.Fixtures;
using PharmaGO.IntegrationTests.Infrastructure.Seeds;
using PharmaGO.IntegrationTests.Infrastructure.Utils;

namespace PharmaGO.IntegrationTests.Infrastructure;

public class IntegrationTestBase
    : IClassFixture<PostgreSqlFixture>, IClassFixture<EnviromentVarsFixture>, IAsyncLifetime
{
    protected readonly IConfiguration Configuration;
    protected readonly PostgreSqlFixture DbFixture;
    protected readonly HttpClient HttpClient;
    protected PharmaGOContext Context => DbFixture.CreateContext();
    protected Pharmacy TestPharmacy;

    protected IntegrationTestBase(PostgreSqlFixture dbFixture, EnviromentVarsFixture envVarsFixture)
    {
        Configuration = envVarsFixture.Configuration;
        DbFixture = dbFixture;

        var factory = new CustomWebApplicationFactory(DbFixture.Container.GetConnectionString());

        HttpClient = factory.CreateClient();
    }

    public virtual async Task InitializeAsync()
    {
        await DbFixture.ResetDatabaseAsync();

        await using var context = Context;
        var seeder = new TestDataSeeder(context, Configuration);
        TestPharmacy = await seeder.SeedAsync();
    }

    public virtual Task DisposeAsync()
    {
        HttpClient.Dispose();
        return Task.CompletedTask;
    }

    protected async Task<Guid> CreateTestPharmacyAsync()
    {
        await using var context = Context;
        var pharmacy = TestDataGenerator.CreatePharmacy();
        await context.Pharmacies.AddAsync(pharmacy);
        await context.SaveChangesAsync();
        return pharmacy.Id;
    }

    protected async Task<string> AuthenticateClientAsync(string email, string password)
    {
        var loginRequest = new { Email = email, Password = password };
        var response = await HttpClient.PostAsJsonAsync("/api/auth/login", loginRequest);
        
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(errorContent);
        }
        
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();
        return result!.Token;
    }

    protected async Task<string> AuthenticateEmployeeAsync(string email, string password)
    {
        var loginRequest = new { Email = email, Password = password };
        var response = await HttpClient.PostAsJsonAsync("/api/auth/admin/login", loginRequest);
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(errorContent);
        }
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();
        return result!.Token;
    }

    protected async Task<string> AuthenticateAdminAsync()
    {
        var adminUserData = Configuration.GetSection("AdminUser");
        var email = adminUserData.GetValue<string>("Email");
        var password = adminUserData.GetValue<string>("Password");
        
        return await AuthenticateEmployeeAsync(email!, password!);
    }

    protected void SetAuthorizationHeader(string token)
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}