using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PharmaGO.Infrastructure.Persistence;
using PharmaGO.Infrastructure.Services;
using PharmaGO.Core.Interfaces.Persistence;
using PharmaGO.Core.Interfaces.Services;
using PharmaGO.Infrastructure.Services.JWT;

namespace PharmaGO.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddDbContext(configuration.GetConnectionString("PharmaGOContext") ??
                              throw new MissingFieldException("ConnectionString databse not found"));
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IPasswordHashingService, PasswordHashingService>();

        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IPharmacyRepository, PharmacyRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}