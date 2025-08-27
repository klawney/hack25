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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
       
        modelBuilder.Entity<SimulacaoData>(entity =>
        {
            entity.Property(e => e.TaxaJuros).HasPrecision(18, 4);
        });

        modelBuilder.Entity<ParcelaData>(entity =>
        {
            entity.Property(e => e.ValorAmortizacao).HasPrecision(18, 4);
            entity.Property(e => e.ValorJuros).HasPrecision(18, 4);
            entity.Property(e => e.ValorPrestacao).HasPrecision(18, 4);
        });
    }
}