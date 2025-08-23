using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Interfaces;

namespace Core;

public class SistemaPrice : ISistemaAmortizacao
{
    public ResultadoSimulacao SimularPorVrTotal(int prazo, decimal taxaJuros, decimal valorTotal)
    {
        if (prazo <= 0 || valorTotal <= 0)
        {
            return new ResultadoSimulacao { Tipo = "PRICE", Parcelas = new List<Parcela>() };
        }

        var valorPrestacao = CalculaValorPrestacao(prazo, taxaJuros, valorTotal);
        return GerarTabelaAmortizacao(prazo, taxaJuros, valorTotal, valorPrestacao);
    }

    public ResultadoSimulacao SimularPorVrPrestacao(int prazo, decimal taxaJuros, decimal valorPrestacao)
    {
        if (prazo <= 0 || valorPrestacao <= 0)
        {
            return new ResultadoSimulacao { Tipo = "PRICE", Parcelas = new List<Parcela>() };
        }
        
        var valorTotal = CalculaValorTotal(prazo, taxaJuros, valorPrestacao);
        return GerarTabelaAmortizacao(prazo, taxaJuros, valorTotal, valorPrestacao);
    }

    private ResultadoSimulacao GerarTabelaAmortizacao(int prazo, decimal taxaJuros, decimal valorEmprestimo, decimal valorPrestacao)
    {
        var saldoDevedor = valorEmprestimo;
        var parcelas = new List<Parcela>();
        var taxaDecimal = taxaJuros / 100m;

        for (int i = 0; i < prazo; i++)
        {
            var juros = Math.Floor((taxaDecimal * saldoDevedor) * 100) / 100m;
            var amortizacao = Math.Floor((valorPrestacao - juros) * 100) / 100m;
            var prestacaoAtual = valorPrestacao;

            if (saldoDevedor - amortizacao < amortizacao)
            {
                amortizacao = saldoDevedor;
                prestacaoAtual = Math.Floor((amortizacao + juros) * 100) / 100m;
            }

            saldoDevedor = Math.Floor((saldoDevedor - amortizacao) * 100) / 100m;
            
            if (i == prazo - 1)
            {
                saldoDevedor = 0;
            }

            parcelas.Add(new Parcela
            {
                Numero = i + 1,
                ValorPrestacao = prestacaoAtual,
                ValorAmortizacao = amortizacao,
                ValorJuros = juros,
                SaldoDevedor = saldoDevedor
            });
        }

        return new ResultadoSimulacao { Tipo = "PRICE", Parcelas = parcelas };
    }

    private decimal CalculaValorPrestacao(int prazo, decimal taxaJuros, decimal valorTotal)
    {
        var taxaDecimal = taxaJuros / 100m;
        var potencia = (decimal)Math.Pow(1.0 + (double)taxaDecimal, -prazo);
        var quociente = 1 - potencia;

        if (quociente == 0) return 0;

        var parcela = valorTotal * taxaDecimal / quociente;
        
        return Math.Floor(parcela * 100) / 100m;
    }

    private decimal CalculaValorTotal(int prazo, decimal taxaJuros, decimal valorPrestacao)
    {
        var taxaDecimal = taxaJuros / 100m;
        if (taxaDecimal == 0) return valorPrestacao * prazo;

        var potencia = (decimal)Math.Pow(1.0 + (double)taxaDecimal, -prazo);
        var numerador = 1m - potencia;
        
        var valorTotal = (numerador / taxaDecimal) * valorPrestacao;

        return decimal.Round(valorTotal, 2);
    }
}