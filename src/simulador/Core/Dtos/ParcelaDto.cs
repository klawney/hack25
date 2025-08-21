namespace Core.Dtos; 
public record ParcelaDto
(
    int Numero,
    decimal ValorAmortizacao,
    decimal ValorJuros,
    decimal ValorPrestacao
);