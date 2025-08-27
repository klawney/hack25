using Microsoft.EntityFrameworkCore;
using Infra.Persistence.Entities;
namespace Infra.Persistence;

public class SimulacaoDbContext : DbContext
{
    public SimulacaoDbContext(DbContextOptions<SimulacaoDbContext> options)
        : base(options)
    {
    }

    public DbSet<SimulacaoData> Simulacoes { get; set; } = default!;
    public DbSet<ResultadoSimulacaoData> ResultadosSimulacao { get; set; } = default!;
    public DbSet<ParcelaData> Parcelas { get; set; } = default!;
}
