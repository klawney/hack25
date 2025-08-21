namespace Core.Dtos;
public record TelemetryResponseDto
(
    DateTime DataReferencia,
    List<EndpointTelemetryDto> ListaEndpoints
);