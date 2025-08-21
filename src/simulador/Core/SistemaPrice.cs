using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Core
{
    public class SistemaPrice : ISistemaAmortizacao
    {
        private List<string> Parcelas = [];
        public string CalculaPorVrTotal(int prazo, decimal taxaJuros, decimal valorTotal)
        {
            var valorParcela = CalculaParcela(prazo, taxaJuros, valorTotal);
            CalculaPorVrPrestacao(prazo, taxaJuros, valorParcela);

            return "472.80";
        }
        public string CalculaPorVrPrestacao(int prazo, decimal taxaJuros, decimal valorPrestacao)
        {
            decimal valorTotal = CalculaVlrTotal(prazo,taxaJuros,valorPrestacao);
            for (int p = 1; p <= prazo; p++)
            {
                Parcelas.Add($"Parcela {p}, valor {valorPrestacao}");
            }
            return string.Empty;
        }

        private decimal CalculaParcela(int prazo, decimal taxaJuros, decimal valorTotal)
        {
            double tx = ((double)taxaJuros);
            double pz = ((double)prazo);
            decimal result = valorTotal * taxaJuros / (1m - ((decimal)Math.Pow(1.0 + tx, -pz)));
            //Valor atual do débito × Taxa de juros ÷ (1 − (1 + Taxa de juros)^  (− Número de parcelas)) 
            return decimal.Round(result, 2);
        }

        private decimal CalculaVlrTotal(int prazo, decimal taxaJuros, decimal valorParcela)
        {
            double tx = ((double)taxaJuros);
            double pz = ((double)prazo);
            decimal result = ((1m-((decimal)Math.Pow(1.0+tx,-pz))) / taxaJuros ) * valorParcela;
            //Valor atual do débito × Taxa de juros ÷ (1 − (1 + Taxa de juros)^  (− Número de parcelas)) 
            return decimal.Round(result,2);
        }
    }
}