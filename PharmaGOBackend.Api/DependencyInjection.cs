using Microsoft.AspNetCore.Mvc.Infrastructure;
using PharmaGOBackend.Api.Mapping;

namespace PharmaGOBackend.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, PharmaGOProblemDetailsFactory>();
        services.AddMappings();

        return services;
    }
}