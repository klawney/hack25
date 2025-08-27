namespace Infra.Persistence.Entities;

public class ParcelaData
{
    public long Id { get; set; }
    public int Numero { get; set; }
    public decimal ValorAmortizacao { get; set; }
    public decimal ValorJuros { get; set; }
    public decimal ValorPrestacao { get; set; }
    
    // Chave estrangeira para ResultadoSimulacaoData
    public long ResultadoSimulacaoId { get; set; }
    
    // Propriedade de navegação para ResultadoSimulacaoData
    public virtual ResultadoSimulacaoData ResultadoSimulacao { get; set; } = default!;
}
