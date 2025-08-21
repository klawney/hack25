namespace Core.Dtos;
public record SimulacaoRegistroDto
(
    long IdSimulacao,
    decimal ValorDesejado,
    int Prazo,
    decimal ValorTotalParcelas
);