using System.Diagnostics.CodeAnalysis;
using Api.Data; // Supondo que ProdutosConnectionFactory esteja aqui
using Api.QueryHandles;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Infra.Persistence;
using Mensageria; // Adicionar using para o produtor e a extensão
using Mensageria.Extensions;
using Api.Services; // Adicionar using para a extensão

[ExcludeFromCodeCoverage]
public static class IoCConfiguration
{
    public static void AddIoC(this IServiceCollection services, IConfiguration configuration)
    {
        // --- Infraestrutura de Banco de Dados ---
        services.AddDbContext<SimulacaoDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("SimulacaoConnection"),
                b => b.MigrationsAssembly("Api") // Corrigido para "Api"
            )
        );
        services.AddSingleton<IDbConnectionFactory, ProdutosConnectionFactory>();

        services.AddEventHubProducer(configuration);
        services.AddScoped<EventHubSimulacaoProducer>();

        // Registra o tracker como Singleton para manter o estado em memória
        services.AddSingleton<IMessageTrackerService, InMemoryMessageTrackerService>();
        services.AddScoped<ISimuladorService, SimuladorService>();
        services.AddScoped<IProdutosQueryHandler, ProdutosQueryHandler>();
    }
}