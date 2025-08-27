
namespace Core.Dtos;
public record SimulacaoResponseDto
(
    long IdSimulacao,
    int CodigoProduto,
    string DescricaoProduto,
    decimal TaxaJuros,
    List<ResultadoSimulacaoDto> ResultadoSimulacao
);
