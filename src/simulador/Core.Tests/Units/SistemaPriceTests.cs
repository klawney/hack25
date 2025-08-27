using Xunit;
using System.Linq;
using Core.Interfaces;

namespace Core.Tests.Units;

public class SistemaPriceTests
{
    private readonly ISistemaAmortizacao _sistemaPrice;

    public SistemaPriceTests()
    {
        _sistemaPrice = new SistemaPrice();
    }

    [Theory]
    // Faixa 1: taxa=1.79%, prazo=24, valor=10000.00
    [InlineData(24, 1.79, 10000.00)]
    // Faixa 2: taxa=1.75%, prazo=36, valor=50000.00 (valor intermediário)
    [InlineData(36, 1.75, 50000.00)]
    // Faixa 3: taxa=1.82%, prazo=96, valor=100000.01
    [InlineData(96, 1.82, 100000.01)]
    // Faixa 4: taxa=1.51%, prazo=120, valor=1500000.00 (prazo > 96)
    [InlineData(120, 1.51, 1500000.00)]
    public void SimularPorPrazoDesejado_ComDadosValidos_DeveZerarSaldoDevedor(int prazo, double taxaJuros, double valorTotal)
    {
        // Arrange
        var taxaDecimal = (decimal)taxaJuros;
        var valorDecimal = (decimal)valorTotal;

        // Act
        var resultado = _sistemaPrice.SimularPorPrazoDesejado(prazo, taxaDecimal, valorDecimal);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("PRICE", resultado.Tipo);
        Assert.NotNull(resultado.Parcelas);
        Assert.Equal(prazo, resultado.Parcelas.Count);
        Assert.Equal(0, resultado.Parcelas.Last().SaldoDevedor);
    }

    [Fact]
    public void SimularPorPrazoDesejado_CasoSimples_ValoresDevemCorresponder()
    {
        // Arrange
        var prazo = 4;
        var taxaJuros = 2.0m; // 2%
        var valorTotal = 1000.00m;

        // Act
        var resultado = _sistemaPrice.SimularPorPrazoDesejado(prazo, taxaJuros, valorTotal);
        var primeiraParcela = resultado.Parcelas.First();

        // Assert
        // Valor da prestação para este cenário: 262.62
        Assert.Equal(262.62m, primeiraParcela.ValorPrestacao);
        Assert.Equal(20.00m, primeiraParcela.ValorJuros);
        Assert.Equal(242.62m, primeiraParcela.ValorAmortizacao);
    }

    [Fact(Skip = "Método não implementado")]
    public void SimularPorVrPrestacaoDesejada_ComDadosValidos_DeveZerarSaldoDevedor()
    {
        // Arrange
        var prazo = 12;
        var taxaJuros = 2.5m;
        var valorPrestacao = 97.49m; // Prestação para um empréstimo de ~1000.00
        var valorTotal = 1000.00m;

        // Act
        var resultado = _sistemaPrice.SimularPorVrPrestacaoDesejada(valorPrestacao, taxaJuros, valorTotal);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(prazo, resultado.Parcelas.Count);
        Assert.Equal(0, resultado.Parcelas.Last().SaldoDevedor);
    }

    [Fact]
    public void Simular_ComPrazoZero_DeveRetornarListaVazia()
    {
        // Arrange
        var prazo = 0;
        var taxaJuros = 2.0m;
        var valorTotal = 10000.0m;

        // Act
        var resultado = _sistemaPrice.SimularPorPrazoDesejado(prazo, taxaJuros, valorTotal);
        
        // Assert
        Assert.NotNull(resultado.Parcelas);
        Assert.Empty(resultado.Parcelas);
    }
}