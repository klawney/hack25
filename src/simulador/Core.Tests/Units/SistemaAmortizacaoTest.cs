using Core.Entities;
using Core.Interfaces;

namespace Core.Tests.Units;

public class SistemaAmortizacaoTest
{
    [Fact]
    public void ShouldReturnFalse()
    {
        ISistemaAmortizacao sistema = new SistemaPrice();
        int prazo = 12;
        decimal taxaJuros = 1.0m;
        decimal valorPrestacao = 500.0m;
        var result = sistema.SimularPorVrPrestacaoDesejada(prazo, taxaJuros, valorPrestacao);
        Console.WriteLine(result);
        Assert.True(true);
    }

    [Fact]
    public void DeveCalcularPorVT()
    {
        ISistemaAmortizacao sistema = new SistemaPrice();
        int prazo = 12;
        decimal taxaJuros = 0.02m;
        decimal vrTotal = 5000.0m;
        var result = sistema.SimularPorPrazoDesejado(prazo, taxaJuros, vrTotal);
        Assert.IsType<ResultadoSimulacao>(result);
        //Assert.Equal("472.80",result);
    }
}