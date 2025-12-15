using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PharmaGO.Application.Common.Auth;
using PharmaGO.Application.Common.Auth.Constants;
using PharmaGO.Core.Common.Constants;

namespace PharmaGO.Api.Authentication;

public static class Config
{
    extension(IServiceCollection services)
    {
        public IServiceCollection ConfigureJwtAuthentication(IConfiguration configuration)
        {
            services.AddAuthentication(opts =>
                    {
                        opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    }
                )
                .AddJwtBearer(opts =>
                {
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(
                                configuration["JwtSettings:Secret"] ??
                                throw new InvalidOperationException("Missing configuration section: JwtSettings:Secret")
                            )
                        ),
                    };
                });

            return services;
        }

        public IServiceCollection ConfigurePermissionsRoles()
        {
            services.AddAuthorizationBuilder()
                .AddPolicy(
                    Policies.MasterAdminOnly,
                    policy => policy.RequireRole(nameof(UserType.MasterAdmin))
                )
                .AddPolicy(
                    Policies.AdminOnly, policy => policy.RequireRole(EmployeeRoles.Admin)
                )
                .AddPolicy(
                    Policies.PharmacyEmployee,
                    policy => policy.RequireRole(EmployeeRoles.All)
                )
                .AddPolicy(
                    Policies.ClientOnly,
                    policy => policy.RequireClaim(
                        CustomClaims.Permission,
                        Permissions.ClientAccess
                    )
                )
                .AddPolicy(
                    Policies.ManageProduct,
                    policy => policy.RequireClaim(
                        CustomClaims.Permission,
                        Permissions.ManageProducts
                    )
                )
                .AddPolicy(
                    Policies.ManageEmployees,
                    policy => policy.RequireClaim(
                        CustomClaims.Permission,
                        Permissions.ManageUsers
                    )
                );

            return services;
        }
    }
}