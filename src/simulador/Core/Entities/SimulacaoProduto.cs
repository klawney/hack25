using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public record SimulacaoProduto
    {
        public long IdSimulacao { get; set; }
        public int CodigoProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public decimal TaxaJuros { get; set; }
        public List<ResultadoSimulacao> ResultadoSimulacao { get; set; } = [];
    }
}