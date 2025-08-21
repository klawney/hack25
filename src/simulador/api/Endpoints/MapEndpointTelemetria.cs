using Microsoft.AspNetCore.Builder;
using Core.Dtos;
using api.Middleware;
using System;
using System.Collections.Generic;

namespace api.Endpoints
{
    public static class TelemetriaEndpointExtensions
    {
        public static WebApplication MapEndpointTelemetria(this WebApplication app)
        {
            app.MapGet("/telemetria", () =>
            {
                // Dados simulados para exemplo. Substitua pelos dados reais do middleware se necessário.
                var qtd = TelemetriaMiddleware.GetQtdeRequisicoes();
                var tempoTotal = TelemetriaMiddleware.GetTempoTotalMs();
                var tempoMedio = qtd > 0 ? (int)(tempoTotal / qtd) : 0;
                var endpoint = new EndpointTelemetryDto(
                    "simular",
                    qtd,
                    tempoMedio,
                    tempoMedio, // Para exemplo, min = max = medio
                    tempoMedio,
                    100m // Sucesso sempre 100% neste exemplo
                );
                var resposta = new TelemetryResponseDto(
                    DateTime.UtcNow,
                    new List<EndpointTelemetryDto> { endpoint }
                );
                return Results.Ok(resposta);
            })
            .WithName("GetTelemetria")
            .WithOpenApi();

            return app;
        }
    }
}
