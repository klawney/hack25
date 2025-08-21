using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ISistemaAmortizacao
    {
        string CalculaPorVrTotal(int prazo, decimal taxaJuros,decimal alorTotal);
        string CalculaPorVrPrestacao(int prazo, decimal taxaJuros, decimal valorPrestacao);
    }
}