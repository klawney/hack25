using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces;

public interface IProdutosQueryHandler
{
    Task<List<Produto>> ListarTodosProdutos();
    Task<List<Produto>> ListarProdutosCompativeis(int prazo, decimal valorDesejado);
    Task<Produto> BuscaProdutoCompativel(int prazo, decimal valorDesejado);
}
