using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PharmaGOBackend.Api.Common;
using PharmaGOBackend.Api.Mapping;

namespace PharmaGOBackend.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        
        services.AddSingleton<ProblemDetailsFactory, PharmaGOProblemDetailsFactory>();

        services.AddFluentValidation();
        services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();

        services.AddMappings();
        return services;
    }
}