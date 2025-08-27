
using System.Diagnostics.CodeAnalysis;
using Core.Dtos;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Api.Services;

namespace Api.Endpoints
{
    [ExcludeFromCodeCoverage]
    public static class EndpointsSimulador
    {
        public static WebApplication MapEndpointSimulacao(this WebApplication app)
        {
            var grupo = app.MapGroup("/simulador").WithDisplayName("Simulação de Empréstimos Pessoais");

            grupo.MapPost("/simular", async ( [FromServices] ISimuladorService service, SimulacaoRequestDto requestDto) =>
            {
                return Results.Ok(await service.RealizarSimulacao(requestDto));
            })
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

