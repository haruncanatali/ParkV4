using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkV4.Application.Common.Interfaces;

namespace ParkV4.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("PostgreSql"))
            );
            services.AddScoped<IApplicationContext>(provider => provider.GetService<ApplicationContext>());
            return services;
        }
    }
}