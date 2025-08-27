namespace Core.Events;

public record SimulacaoGeradaParaEventHub
(
    long IdSimulacao,
    int CodigoProduto,
    string DescricaoProduto,
    decimal TaxaJuros,
    List<ResultadoSimulacaoEventHub> ResultadoSimulacao
);

public record ResultadoSimulacaoEventHub
(
    string? Tipo,
    List<ParcelaEventHub> Parcelas
);

public record ParcelaEventHub
(
    int Numero,
    decimal ValorAmortizacao,
    decimal ValorJuros,
    decimal ValorPrestacao
);
