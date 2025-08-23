using System.Data;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace api.QueryHandles
{
    public class ProdutosQueryHandler : IProdutosQueryHandler
    {
        private readonly IDbConnection _dbConnection;

        public ProdutosQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // O método recebe o objeto Query, tornando a assinatura mais limpa e explícita.
        public async Task<IEnumerable<Produto>> ExecuteQueryAsync(SimulacaoRequestDto query)
        {
            const string sql1 = @"
            SELECT 
                Id, Nome, TaxaDeJurosAnual, ValorMinimo, ValorMaximo, PrazoMinimoMeses, PrazoMaximoMeses
            FROM 
                Produtos
            WHERE 
                @ValorDesejado BETWEEN ValorMinimo AND ValorMaximo
                AND @Prazo BETWEEN PrazoMinimoMeses AND PrazoMaximoMeses;
            ";

            try
            {
               var sql = "SELECT GETDATE() dt;";
                // Usamos as propriedades do objeto query para os parâmetros do Dapper.<Produto>
                var produtos = await _dbConnection.QueryAsync(sql);//,
                   // new { ValorDesejado = "asdas", Prazo = "sadas" });
                  //  new { ValorDesejado = query.ValorDesejado, Prazo = query.Prazo });
            }
            catch (SqlException ex)
            {
                var v = ex;
                throw;
            }
            catch (Exception ex)
            {
                var v = ex;
                throw;
            }

            return new List<Produto>(); //produtos;
        }
    }
}