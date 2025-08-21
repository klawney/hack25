namespace Core.Dtos;
public record DetalhesSimulacaoDiariaDto
(
    int CodigoProduto,
    string DescricaoProduto,
    decimal TaxaMediaJuro,
    decimal ValorMedioPrestacao,
    decimal ValorTotalDesejado,
    decimal ValorTotalCredito
);