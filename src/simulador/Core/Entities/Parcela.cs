using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class Parcela
{
    public int Numero { get; set; }
    public decimal ValorPrestacao { get; set; }
    public decimal ValorAmortizacao { get; set; }
    public decimal ValorJuros { get; set; }
    public decimal SaldoDevedor { get; set; }
}