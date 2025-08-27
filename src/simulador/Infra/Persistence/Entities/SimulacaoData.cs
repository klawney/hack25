namespace Infra.Persistence.Entities;

public class SimulacaoData
{
    public long Id { get; set; }
    public long IdSimulacao { get; set; }
    public int CodigoProduto { get; set; }
    public string DescricaoProduto { get; set; } = default!;
    public decimal TaxaJuros { get; set; }

    // Propriedade de navegação para ResultadoSimulacaoData
    public virtual ICollection<ResultadoSimulacaoData> Resultados { get; set; } = new List<ResultadoSimulacaoData>();
}
