using Core.Dtos;
using Infra.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Api.QueryHandles;

public interface IListarSimulacoesQueryHandler
{
    Task<PaginacaoResponseDto> HandleAsync(int pagina = 1, int tamanhoPagina = 10);
}

public class ListarSimulacoesQueryHandler : IListarSimulacoesQueryHandler
{
    private readonly SimulacaoDbContext _dbContext;

    public ListarSimulacoesQueryHandler(SimulacaoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaginacaoResponseDto> HandleAsync(int pagina = 1, int tamanhoPagina = 10)
    {
        if (pagina < 1) pagina = 1;
        if (tamanhoPagina < 1) tamanhoPagina = 10;
        if (tamanhoPagina > 100) tamanhoPagina = 100;

        var totalRegistros = await _dbContext.Simulacoes.CountAsync();

        // Passo 1: Trazer os dados paginados para a memória
        var simulacoesNoBanco = await _dbContext.Simulacoes
            .AsNoTracking()
            .OrderByDescending(s => s.Id)
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            // Inclui os dados relacionados necessários para o cálculo
            .Include(s => s.Resultados)
            .ThenInclude(r => r.Parcelas)
            .ToListAsync();

        // Passo 2: Mapear para o DTO em memória (LINQ to Objects)
        var registrosDto = simulacoesNoBanco.Select(s => new SimulacaoRegistroDto(
            IdSimulacao: s.Id,
            ValorDesejado: s.Resultados.SelectMany(r => r.Parcelas).Sum(p => p.ValorAmortizacao),
            Prazo: s.Resultados.FirstOrDefault()?.Parcelas.Count ?? 0,
            ValorTotalParcelas: s.Resultados.SelectMany(r => r.Parcelas).Sum(p => p.ValorPrestacao)
        )).ToList();
            
        return new PaginacaoResponseDto(
            Pagina: pagina,
            QtdRegistros: totalRegistros,
            QtdRegistrosPagina: registrosDto.Count,
            Registros: registrosDto
        );
    }
}