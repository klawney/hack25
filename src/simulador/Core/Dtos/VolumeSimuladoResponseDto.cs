namespace Core.Dtos;
public record VolumeSimuladoResponseDto
(
    DateTime DataReferencia,
    List<DetalhesSimulacaoDiariaDto> Simulacoes
);
