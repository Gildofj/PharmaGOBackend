using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PharmaGOBackend.Core.Interfaces.Authentication;
using PharmaGOBackend.Core.Interfaces.Persistence;
using PharmaGOBackend.Core.Interfaces.Services;
using PharmaGOBackend.Infrastructure.Authentication;
using PharmaGOBackend.Infrastructure.Persistence;
using PharmaGOBackend.Infrastructure.Services;

namespace PharmaGOBackend.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddDbContext(configuration.GetConnectionString("PharmaGOContext") ??
                              throw new MissingFieldException("ConnectionString databse not found"));
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IPharmacyRepository, PharmacyRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
    }
}