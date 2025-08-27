namespace Infra.Persistence.Entities;
public class ResultadoSimulacaoData
{
    public long Id { get; set; }
    public string? Tipo { get; set; }
    
    // Chave estrangeira para SimulacaoData
    public long SimulacaoId { get; set; }
    
    // Propriedade de navegação para SimulacaoData
    public virtual SimulacaoData Simulacao { get; set; } = default!;
    
    // Propriedade de navegação para ParcelaData
    public virtual ICollection<ParcelaData> Parcelas { get; set; } = new List<ParcelaData>();
}
