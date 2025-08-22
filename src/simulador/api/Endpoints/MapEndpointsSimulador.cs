
using System.Diagnostics.CodeAnalysis;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Endpoints
{
    [ExcludeFromCodeCoverage]
    public static class EndpointsSimulador
    {
        public static WebApplication MapEndpointSimulacao(this WebApplication app)
        {
            var grupo = app.MapGroup("/simulador");
                
            // grupo.MapGet("/minhas-simulacoes", (ISimuladorService service) => "minhas-simulacoes")
            //     .WithName("MinhasSimulacoes")
            //     .WithOpenApi();

            // grupo.MapGet("/simular", (ISimuladorService service) => "simulando...")
            //     .WithName("Simular")
            //     .WithOpenApi();

            grupo.MapGet("/realizadas", () => "realizadas")
                .WithName("SimulacoesRealizadas")
                .WithOpenApi();

            grupo.MapGet("/diarias-produtos", () => "diarias-produtos")
                .WithName("SimulacoesDiariasProdutos")
                .WithOpenApi();
            return app;
        }
    }
}

