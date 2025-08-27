// api/Data/ProdutosConnectionFactory.cs

using System.Data;
using Core.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Api.Data
{
    public class ProdutosConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public ProdutosConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            var connectionString = _configuration.GetConnectionString("ProdutosConnection");
            return new SqlConnection(connectionString);
        }
    }
}