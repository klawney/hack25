using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Core
{
    public class SistemaSac : ISistemaAmortizacao
    {
        public string CalculaPorVrTotal(int prazo, decimal taxaJuros, decimal alorTotal)
        {
            return string.Empty;
        }
        public string CalculaPorVrPrestacao(int prazo, decimal taxaJuros, decimal valorPrestacao)
        {
            return string.Empty;
        }
    }
}