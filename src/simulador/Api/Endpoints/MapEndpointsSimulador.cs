
using System.Diagnostics.CodeAnalysis;
using Core.Dtos;
using Core.Interfaces;
using Api.QueryHandles;
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

            grupo.MapGet("/realizadas", async (
                [FromServices] IListarSimulacoesQueryHandler handler,
                [FromQuery] int? pagina,
                [FromQuery] int? tamanhoPagina) =>
            {
                var resultado = await handler.HandleAsync(pagina ?? 1, tamanhoPagina ?? 10);
                return Results.Ok(resultado);
            })
            .WithName("SimulacoesRealizadas")
            .WithOpenApi();

            grupo.MapGet("/minhas-simulacoes", () => "minhas-simulacoes solicitadas")
                .WithName("Minhas Simulacoes")
                .WithOpenApi();


            grupo.MapGet("/diarias-produtos", async (
                [FromServices] IGerarRelatorioDiarioQueryHandler handler,
                [FromQuery] DateTime? data) =>
            {
                var resultado = await handler.HandleAsync(data);
                return Results.Ok(resultado);
            })
            .WithName("SimulacoesDiariasProdutos")
            .WithOpenApi();
            
            return app;
        }
    }
}

