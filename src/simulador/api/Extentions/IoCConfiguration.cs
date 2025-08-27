using System.Diagnostics.CodeAnalysis;
using Api.Data;
using Api.QueryHandles;
using Core.Interfaces;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using Infra.Persistence;


[ExcludeFromCodeCoverage]
public static class IoCConfiguration
{
    public static void AddIoC(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuração do DbContext
        services.AddDbContext<SimulacaoDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("SimulacaoConnection"),
                b => b.MigrationsAssembly("api")
            )
        );
        // Serviços da aplicação
        services.AddScoped<ISimuladorService, SimuladorService>();
        services.AddScoped<IProdutosQueryHandler, ProdutosQueryHandler>();
        services.AddSingleton<IDbConnectionFactory, ProdutosConnectionFactory>();
    }
}