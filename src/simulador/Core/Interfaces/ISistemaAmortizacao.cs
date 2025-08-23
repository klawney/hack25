using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces;

public interface ISistemaAmortizacao
{
    ResultadoSimulacao SimularPorVrTotal(int prazo, decimal taxaJuros, decimal valorTotal);
    ResultadoSimulacao SimularPorVrPrestacao(int prazo, decimal taxaJuros, decimal valorPrestacao);
}