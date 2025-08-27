using Xunit;
using System.Linq;
using Core.Interfaces;

namespace Core.Tests.Units;

public class SistemaSacTests
{
    private readonly ISistemaAmortizacao _sistemaSac;

    public SistemaSacTests()
    {
        _sistemaSac = new SistemaSac();
    }

    [Theory]
    // Faixa 1: taxa=1.79%, prazo=24, valor=10000.00
    [InlineData(24, 1.79, 10000.00)]
    // Faixa 2: taxa=1.75%, prazo=48, valor=100000.00
    [InlineData(48, 1.75, 100000.00)]
    // Faixa 3: taxa=1.82%, prazo=49, valor=100000.01
    [InlineData(49, 1.82, 100000.01)]
    // Faixa 4: taxa=1.51%, prazo=97, valor=1000000.01
    [InlineData(97, 1.51, 1000000.01)]
    public void SimularPorPrazoDesejado_ComDadosValidos_DeveZerarSaldoDevedor(int prazo, double taxaJuros, double valorTotal)
    {
        // Arrange
        var taxaDecimal = (decimal)taxaJuros;
        var valorDecimal = (decimal)valorTotal;

        // Act
        var resultado = _sistemaSac.SimularPorPrazoDesejado(prazo, taxaDecimal, valorDecimal);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("SAC", resultado.Tipo);
        Assert.NotNull(resultado.Parcelas);
        Assert.Equal(prazo, resultado.Parcelas.Count);
        Assert.Equal(0, resultado.Parcelas.Last().SaldoDevedor);
    }

    [Fact]
    public void SimularPorPrazoDesejado_CasoSimples_ValoresDevemCorresponder()
    {
        // Arrange
        var prazo = 4;
        var taxaJuros = 10.0m; // 10% para facilitar o cálculo
        var valorTotal = 1000.00m;
        
        // Act
        var resultado = _sistemaSac.SimularPorPrazoDesejado(prazo, taxaJuros, valorTotal);
        var primeiraParcela = resultado.Parcelas.First();
        var segundaParcela = resultado.Parcelas[1];

        // Assert
        // Amortização constante: 1000 / 4 = 250
        Assert.Equal(250.00m, primeiraParcela.ValorAmortizacao);
        Assert.Equal(250.00m, segundaParcela.ValorAmortizacao);
        
        // Juros 1: 1000 * 10% = 100. Prestação 1 = 250 + 100 = 350
        Assert.Equal(100.00m, primeiraParcela.ValorJuros);
        Assert.Equal(350.00m, primeiraParcela.ValorPrestacao);
        
        // Juros 2: (1000 - 250) * 10% = 75. Prestação 2 = 250 + 75 = 325
        Assert.Equal(75.00m, segundaParcela.ValorJuros);
        Assert.Equal(325.00m, segundaParcela.ValorPrestacao);
    }
    //pule este teste
    [Fact(Skip = "Implementação futura")]
    public void SimularPorVrPrestacaoDesejada_ComDadosValidos_DeveZerarSaldoDevedor()
    {
        // Arrange
        var prazo = 4;
        var taxaJuros = 10.0m;
        var valorPrimeiraPrestacao = 350.00m; // Prestação para um empréstimo de 1000.00

        // Act
        var resultado = _sistemaSac.SimularPorVrPrestacaoDesejada(prazo, taxaJuros, valorPrimeiraPrestacao);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(prazo, resultado.Parcelas.Count);
        Assert.Equal(0, resultado.Parcelas.Last().SaldoDevedor);
    }
}