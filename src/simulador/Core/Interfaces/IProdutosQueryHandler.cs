using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces;

public interface IProdutosQueryHandler
{
    Task<IEnumerable<Produto>> ExecuteQueryAsync(SimulacaoRequestDto parametros);
}
