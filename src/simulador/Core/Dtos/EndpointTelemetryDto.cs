namespace Core.Dtos;
public record EndpointTelemetryDto
(
    string NomeApi,
    int QtdRequisicoes,
    int TempoMedio,
    int TempoMinimo,
    int TempoMaximo,
    decimal PercentualSucesso
);
