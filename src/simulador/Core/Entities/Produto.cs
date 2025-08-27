using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;
public record Produto
{
    public int CoProduto { get; init; }
    public string NoProduto { get; init; }
    public decimal PcTaxaJuros { get; init; }
    public int NuMinimoMeses { get; init; }
    public int? NuMaximoMeses { get; init; }
    public decimal VrMinimo { get; init; }
    public decimal? VrMaximo { get; init; }
}
