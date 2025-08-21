using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace api.Extensions
{
    public static class EfMssqlExtensions
    {
        public static IServiceCollection AddMssqlEf<TContext>(this IServiceCollection services, IConfiguration config, string connectionName = "DefaultConnection") where TContext : DbContext
        {
            var connectionString = config.GetConnectionString(connectionName);
            services.AddDbContext<TContext>(options =>
                options.UseSqlServer(connectionString));
            return services;
        }
    }
}
