
using System.Diagnostics.CodeAnalysis;
using Core.Dtos;
using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Endpoints
{
    [ExcludeFromCodeCoverage]
    public static class EndpointsSimulador
    {
        public static WebApplication MapEndpointSimulacao(this WebApplication app)
        public static WebApplication MapEndpointSimulacao(this WebApplication app)
        {
            var grupo = app.MapGroup("/simulador").WithDisplayName("Simulação de Empréstimos Pessoais");

            grupo.MapPost("/simular", ( [FromServices] ISimuladorService service, SimulacaoRequestDto requestDto) =>
                Results.Ok( service.RealizarSimulacao(requestDto)))
                .WithName("Simular")
                .WithOpenApi();
            
            grupo.MapGet("/minhas-simulacoes", () => "minhas-simulacoes solicitadas")
                .WithName("Minhas Simulacoes")
                .WithOpenApi();

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

