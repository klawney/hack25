using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Core.Entities;

public class Simulacao
{
    private int Prazo { get; init; }
    private decimal TaxaJuros { get; init; }
    private decimal ValorTotal { get; init; }
    private decimal ValorPrestacao { get; init; }
    public Simulacao(int prazo, decimal taxaJuros, decimal valorTotal = 0, decimal valorPrestacao = 0)
    {
        Prazo = prazo;
        TaxaJuros = taxaJuros;
        ValorTotal = valorTotal;
        ValorPrestacao = valorPrestacao;
    }

    // public ResultadoSimulacao GeraSimulacao(ISistemaAmortizacao sistema)
    // {
    //     ResultadoSimulacao simT = sistema.SimularPorVrTotal( Prazo, TaxaJuros, valorTotal);
    //     ResultadoSimulacao simP = sistema.SimularPorVrPrestacao( Prazo, TaxaJuros, ValorPrestacao);
    // }
}