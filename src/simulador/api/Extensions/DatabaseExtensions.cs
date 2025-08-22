using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;


namespace api.Extensions
{
    public static class DatabaseExtensions
    {
        // Extensão para Entity Framework com SQL Server
        public static IServiceCollection AddMssqlEf<TContext>(this IServiceCollection services, IConfiguration config, string connectionName = "SqlServerConnection") where TContext : DbContext
        {
            var connectionString = config.GetConnectionString(connectionName);
            services.AddDbContext<TContext>(options =>
                options.UseSqlServer(connectionString));
            return services;
        }

        // Extensão para Dapper com SQL Server
        public static IServiceCollection AddMssqlDapper(this IServiceCollection services, IConfiguration config, string connectionName = "SqlServerConnection")
        {
            var connectionString = config.GetConnectionString(connectionName);
            services.AddScoped<IDbConnection>(sp => new SqlConnection(connectionString));
            return services;
        }
    }
}
