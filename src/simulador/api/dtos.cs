public record SimulacaoRequest
(
    decimal ValorDesejado,
    int Prazo
);

Detalhes da Parcela (SAC e PRICE)

Esta classe é usada por ambos os tipos de simulação (SAC e PRICE).
public record Parcela
(
    int Numero,
    decimal ValorAmortizacao,
    decimal ValorJuros,
    decimal ValorPrestacao
);

public record ResultadoSimulacao
(
    string Tipo,
    List<Parcela> Parcelas
   // List<Parcela> ParcelasSacPrice
);

public record SimulacaoResponse
(
    long IdSimulacao,
    int CodigoProduto,
    string DescricaoProduto,
    decimal TaxaJuros,
    List<ResultadoSimulacao> ResultadoSimulacao
);

public record SimulacaoRegistro
(
    long IdSimulacao,
    decimal ValorDesejado,
    int Prazo,
    decimal ValorTotalParcelas
);

public record PaginacaoResponse
(
    int Pagina,
    int QtdRegistros,
    int QtdRegistrosPagina,
    List<SimulacaoRegistro> Registros
);

public record DetalhesSimulacaoDiaria
(
    int CodigoProduto,
    string DescricaoProduto,
    decimal TaxaMediaJuro,
    decimal ValorMedioPrestacao,
    decimal ValorTotalDesejado,
    decimal ValorTotalCredito
);

public record VolumeSimuladoResponse
(
    DateTime DataReferencia,
    List<DetalhesSimulacaoDiaria> Simulacoes
);

//Classes para o Modelo de Telemetria
public record EndpointTelemetry
(
    string NomeApi,
    int QtdRequisicoes,
    int TempoMedio,
    int TempoMinimo,
    int TempoMaximo,
    decimal PercentualSucesso
);

// Envelope de Retorno de Telemetria
public record TelemetryResponse
(
    DateTime DataReferencia,
    List<EndpointTelemetry> ListaEndpoints
);