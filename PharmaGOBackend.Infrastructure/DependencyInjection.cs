using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PharmaGOBackend.Application.Common.Interfaces.Authentication;
using PharmaGOBackend.Application.Common.Interfaces.Persistence;
using PharmaGOBackend.Application.Common.Interfaces.Services;
using PharmaGOBackend.Infrastructure.Authentication;
using PharmaGOBackend.Infrastructure.Persistence;
using PharmaGOBackend.Infrastructure.Services;

namespace PharmaGOBackend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddDbContext(configuration.GetConnectionString("PharmaGOContext"));
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IPharmacyRepository, PharmacyRepository>();

        return services;
    }
}
