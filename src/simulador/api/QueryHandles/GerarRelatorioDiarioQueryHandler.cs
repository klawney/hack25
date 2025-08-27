using Core.Dtos;
using Infra.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Api.QueryHandles;

public interface IGerarRelatorioDiarioQueryHandler
{
    Task<VolumeSimuladoResponseDto> HandleAsync(DateTime? dataReferencia);
}

public class GerarRelatorioDiarioQueryHandler : IGerarRelatorioDiarioQueryHandler
{
    private readonly SimulacaoDbContext _dbContext;

    public GerarRelatorioDiarioQueryHandler(SimulacaoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<VolumeSimuladoResponseDto> HandleAsync(DateTime? dataReferencia)
    {
        var dataFiltro = dataReferencia?.Date ?? DateTime.UtcNow.Date;

        // Passo 1: Trazer os dados relevantes para a memória.
        var todasAsSimulacoes = await _dbContext.Simulacoes
            .AsNoTracking()
            .Select(s => new 
            {
                s.CodigoProduto,
                s.DescricaoProduto,
                s.TaxaJuros,
                Resultados = s.Resultados.Select(r => new 
                {
                    Parcelas = r.Parcelas.Select(p => new 
                    {
                        p.ValorPrestacao,
                        p.ValorAmortizacao
                    })
                })
            })
            .ToListAsync();

        // Passo 2: Agrupar e agregar em memória (LINQ to Objects)
        var detalhesFinais = todasAsSimulacoes
            .GroupBy(s => new { s.CodigoProduto, s.DescricaoProduto })
            .Select(g => new DetalhesSimulacaoDiariaDto(
                CodigoProduto: g.Key.CodigoProduto,
                DescricaoProduto: g.Key.DescricaoProduto,
                TaxaMediaJuro: g.Average(s => s.TaxaJuros),
                ValorMedioPrestacao: g.SelectMany(s => s.Resultados)
                                      .SelectMany(r => r.Parcelas)
                                      .DefaultIfEmpty() // Evita erro se não houver parcelas
                                      .Average(p => p?.ValorPrestacao ?? 0),
                ValorTotalDesejado: g.SelectMany(s => s.Resultados)
                                     .SelectMany(r => r.Parcelas)
                                     .Sum(p => p.ValorAmortizacao),
                ValorTotalCredito: g.SelectMany(s => s.Resultados)
                                    .SelectMany(r => r.Parcelas)
                                    .Sum(p => p.ValorPrestacao)
            ))
            .ToList();

        return new VolumeSimuladoResponseDto(
            dataFiltro,
            detalhesFinais
        );
    }
}