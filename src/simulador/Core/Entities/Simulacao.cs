using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Core.Entities
{
    public class Simulacao
    {
        private int Prazo { get; init; }
        private decimal TaxaJuros { get; init; }
        private decimal ValorTotal { get; init; }
        private decimal ValorPrestacao { get; init; }
        public Simulacao(int prazo, decimal taxaJuros, decimal valorTotal=0, decimal valorPrestacao=0)
        {
            Prazo = prazo;
            TaxaJuros = taxaJuros;
            ValorTotal = valorTotal;
            ValorPrestacao = valorPrestacao;
        }


        public string GeraSimulacao(ISistemaAmortizacao sistema)
        {
            if (ValorTotal > 0)
            {
            return sistema.CalculaPorVrTotal(Prazo,TaxaJuros,ValorTotal);
                
            }
            return sistema.CalculaPorVrPrestacao(Prazo,TaxaJuros,ValorPrestacao);
        }
    }
}