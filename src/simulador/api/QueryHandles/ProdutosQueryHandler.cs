// Api.QueryHandles/ProdutosQueryHandler.cs

using System.Data;
using Core.Entities;
using Core.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Api.QueryHandles;

public class ProdutosQueryHandler : IProdutosQueryHandler
{
    // Dependa da fábrica
    private readonly IDbConnectionFactory _dbConnectionFactory;
    public ProdutosQueryHandler(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }
    public async Task<List<Produto>> ListarTodosProdutos()
    {
        const string sql = @"SELECT                    
                    CO_PRODUTO AS CoProduto,
                    NO_PRODUTO AS NoProduto,
                    PC_TAXA_JUROS AS PcTaxaJuros,
                    NU_MINIMO_MESES AS NuMinimoMeses,
                    NU_MAXIMO_MESES AS NuMaximoMeses,
                    VR_MINIMO AS VrMinimo,
                    VR_MAXIMO AS VrMaximo
                FROM dbo.PRODUTO;";

        try
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            var produtos = await connection.QueryAsync<Produto>(sql);
            return produtos.ToList();
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"[DEBUG] SQL Error: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[DEBUG] Error: {ex.Message}");
            throw;
        }
    }
    public async Task<List<Produto>> ListarProdutosCompativeis(int prazo, decimal valorDesejado)
    {
        const string sql = @"SELECT                    
                    CO_PRODUTO AS CoProduto,
                    NO_PRODUTO AS NoProduto,
                    PC_TAXA_JUROS AS PcTaxaJuros,
                    NU_MINIMO_MESES AS NuMinimoMeses,
                    NU_MAXIMO_MESES AS NuMaximoMeses,
                    VR_MINIMO AS VrMinimo,
                    VR_MAXIMO AS VrMaximo
                FROM dbo.PRODUTO
                WHERE  @Valor BETWEEN VR_MINIMO AND VR_MAXIMO
                    AND @Meses BETWEEN NU_MINIMO_MESES AND NU_MAXIMO_MESES
                    OR (@Meses >= NU_MINIMO_MESES AND NU_MAXIMO_MESES IS NULL AND @Valor > VR_MINIMO AND VR_MAXIMO IS NULL);";
        try
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            var produtos = await connection.QueryAsync<Produto>(sql, new { Valor = valorDesejado, Meses = prazo });
            return produtos.ToList();
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"[DEBUG] SQL Error: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[DEBUG] Error: {ex.Message}");
            throw;
        }
    }
    public async Task<Produto> BuscaProdutoCompativel(int prazo, decimal valorDesejado)
    {
        const string sql = @"SELECT                    
                    CO_PRODUTO AS CoProduto,
                    NO_PRODUTO AS NoProduto,
                    PC_TAXA_JUROS AS PcTaxaJuros,
                    NU_MINIMO_MESES AS NuMinimoMeses,
                    NU_MAXIMO_MESES AS NuMaximoMeses,
                    VR_MINIMO AS VrMinimo,
                    VR_MAXIMO AS VrMaximo
                FROM dbo.PRODUTO
                WHERE  @Valor BETWEEN VR_MINIMO AND VR_MAXIMO
                    AND @Meses BETWEEN NU_MINIMO_MESES AND NU_MAXIMO_MESES
                    OR (@Meses >= NU_MINIMO_MESES AND NU_MAXIMO_MESES IS NULL AND @Valor > VR_MINIMO AND VR_MAXIMO IS NULL);";
        try
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            var produto = await connection.QueryFirstOrDefaultAsync<Produto>(sql, new { Valor = valorDesejado, Meses = prazo });
            return produto; 
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"[DEBUG] SQL Error: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[DEBUG] Error: {ex.Message}");
            throw;
        }
    }
}