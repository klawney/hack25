using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Interfaces;

namespace Core;

public class SistemaSac : ISistemaAmortizacao
{
    public ResultadoSimulacao SimularPorVrTotal(int prazo, decimal taxaJuros, decimal valorTotal)
    {
        if (prazo <= 0 || valorTotal <= 0)
        {
            return new ResultadoSimulacao { Tipo = "SAC", Parcelas = new List<Parcela>() };
        }

        var parcelas = new List<Parcela>();
        var saldoDevedor = valorTotal;
        var taxaDecimal = taxaJuros / 100m;
        var amortizacaoConstante = Math.Floor((valorTotal / prazo) * 100) / 100m;

        for (int i = 0; i < prazo; i++)
        {
            var juros = Math.Floor((taxaDecimal * saldoDevedor) * 100) / 100m;
            var amortizacaoAtual = amortizacaoConstante;

            if (saldoDevedor - amortizacaoAtual < amortizacaoAtual)
            {
                amortizacaoAtual = saldoDevedor;
            }

            var prestacao = Math.Floor((amortizacaoAtual + juros) * 100) / 100m;
            saldoDevedor = Math.Floor((saldoDevedor - amortizacaoAtual) * 100) / 100m;
            
            if (i == prazo - 1)
            {
                saldoDevedor = 0;
            }

            parcelas.Add(new Parcela
            {
                Numero = i + 1,
                ValorPrestacao = prestacao,
                ValorAmortizacao = amortizacaoAtual,
                ValorJuros = juros,
                SaldoDevedor = saldoDevedor
            });
        }

        return new ResultadoSimulacao { Tipo = "SAC", Parcelas = parcelas };
    }

    public ResultadoSimulacao SimularPorVrPrestacao(int prazo, decimal taxaJuros, decimal valorPrestacao)
    {
        if (prazo <= 0 || valorPrestacao <= 0)
        {
            return new ResultadoSimulacao { Tipo = "SAC", Parcelas = new List<Parcela>() };
        }

        var taxaDecimal = taxaJuros / 100m;
        var divisor = (1m / prazo) + taxaDecimal;

        if (divisor == 0) return new ResultadoSimulacao { Tipo = "SAC", Parcelas = new List<Parcela>() };
        
        var valorTotalCalculado = decimal.Round(valorPrestacao / divisor, 2);
        
        return SimularPorVrTotal(prazo, taxaJuros, valorTotalCalculado);
    }
}