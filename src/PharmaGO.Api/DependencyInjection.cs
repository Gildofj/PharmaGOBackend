using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi;
using PharmaGO.Api.Errors;
using PharmaGO.Api.Mapping;
using PharmaGO.Core.Entities;
using Scalar.AspNetCore;

namespace PharmaGO.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddOData(options =>
                options
                    .EnableQueryFeatures(100)
                    .AddRouteComponents("odata", GetEdmModel())
            );
        services.AddSingleton<ProblemDetailsFactory, PharmaGOProblemDetailsFactory>();
        services.AddMappings();

        return services;
    }

    private static IEdmModel GetEdmModel()
    {
        var builder = new ODataConventionModelBuilder();

        builder.EntitySet<Employee>("Employees");
        builder.EntitySet<Client>("Clients");
        builder.EntitySet<Product>("Products");
        builder.EntitySet<Pharmacy>("Pharmacies");

        return builder.GetEdmModel();
    }

    public static IServiceCollection AddOpenApiDefault(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Info = new OpenApiInfo
                {
                    Version = "v1",
                    Title = "PharmaGO API",
                    Description = "The API to handle Pharmacy ECommerce",
                    Contact = new OpenApiContact
                    {
                        Name = "Gildo Junior",
                        Email = "1gildojunior@gmail.com",
                        Url = new Uri("https://gildofj.github.io/portfolio")
                    }
                };
                return Task.CompletedTask;
            });

            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Components ??= new OpenApiComponents();
                document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

                document.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                };
                return Task.CompletedTask;
            });

            options.AddOperationTransformer((operation, context, cancellationToken) =>
            {
                operation.Security ??= new List<OpenApiSecurityRequirement>();

                operation.Security.Add(new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Bearer")] = []
                });

                return Task.CompletedTask;
            });
        });

        return services;
    }

    public static void UseOpenApiDefault(this WebApplication app)
    {
        app.MapOpenApi();

        app.MapScalarApiReference(options =>
        {
            options
                .WithTitle("PharmaGO API")
                .WithTheme(ScalarTheme.Purple)
                .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
        });
    }
}