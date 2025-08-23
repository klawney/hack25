using System.Diagnostics.CodeAnalysis;
using api.Extensions;
using api.QueryHandles;
using Core.Interfaces;
using Core.Services;

namespace Infra.Configurations;

[ExcludeFromCodeCoverage]
public static class IoCConfiguration
{
    public static void AddIoC(this IServiceCollection services)
    {
        services.AddScoped<ISimuladorService, SimuladorService>();
        services.AddScoped<IProdutosQueryHandler, ProdutosQueryHandler>();
    }
}
