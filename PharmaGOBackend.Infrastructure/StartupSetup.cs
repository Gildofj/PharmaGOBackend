using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PharmaGOBackend.Infrastructure.Persistence;

namespace PharmaGOBackend.Infrastructure
{
    public static class StartupSetup
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString) =>
            services.AddDbContext<PharmaGOContext>(options =>
                options.UseNpgsql(connectionString));
    }
}
