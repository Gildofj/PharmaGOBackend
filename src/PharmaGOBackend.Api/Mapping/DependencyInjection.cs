using System.Reflection;
using Mapster;
using MapsterMapper;

namespace PharmaGOBackend.Api.Mapping;

public static class DependencyInjection
{
    public static void AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
    }
}