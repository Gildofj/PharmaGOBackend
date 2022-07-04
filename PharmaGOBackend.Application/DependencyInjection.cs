using Microsoft.Extensions.DependencyInjection;
using PharmaGOBackend.Application.Services.Authentication;

namespace PharmaGOBackend.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
