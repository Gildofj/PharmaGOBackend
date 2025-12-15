using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PharmaGO.Application.Common.Interfaces;
using PharmaGO.Infrastructure.Persistence;
using PharmaGO.Infrastructure.Services;
using PharmaGO.Core.Interfaces.Persistence;
using PharmaGO.Core.Interfaces.Services;
using PharmaGO.Infrastructure.Services.JWT;

namespace PharmaGO.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration
    )
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddDbContext(configuration.GetConnectionString("PharmaGOContext") ??
                              throw new MissingFieldException("ConnectionString databse not found"));
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>(opts =>
                {
                    opts.Password.RequiredLength = 8;
                    opts.Password.RequireDigit = true;
                    opts.Password.RequireUppercase = true;
                    opts.Password.RequireNonAlphanumeric = true;
                    opts.Password.RequireLowercase = true;
                }
            )
            .AddEntityFrameworkStores<PharmaGOContext>()
            .AddDefaultTokenProviders();

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IPermissionService, PermissionService>();

        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IPharmacyRepository, PharmacyRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}