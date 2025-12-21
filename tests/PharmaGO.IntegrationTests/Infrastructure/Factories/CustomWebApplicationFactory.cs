using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PharmaGO.Infrastructure.Persistence;

namespace PharmaGO.IntegrationTests.Infrastructure.Factories;

public class CustomWebApplicationFactory(string connectionString) : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<PharmaGOContext>>();
            services.RemoveAll<PharmaGOContext>();

            services.AddDbContext<PharmaGOContext>(options =>
            {
                options.UseNpgsql(connectionString);
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            });

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var context = scopedServices.GetRequiredService<PharmaGOContext>();

            context.Database.EnsureCreated();
        });

        builder.UseEnvironment("Test");
    }
}