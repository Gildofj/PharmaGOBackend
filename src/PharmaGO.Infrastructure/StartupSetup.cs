using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PharmaGO.Infrastructure.Persistence;

namespace PharmaGO.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<PharmaGOContext>(options => options.UseNpgsql(connectionString));
        }
    }
}