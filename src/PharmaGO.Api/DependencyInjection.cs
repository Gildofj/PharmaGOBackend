using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using PharmaGO.Api.Errors;
using PharmaGO.Api.Mapping;
using PharmaGO.Core.Entities;

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
}