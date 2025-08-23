using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Core.Entities;

public record ResultadoSimulacao
{
    public string? Tipo { get; set; }
    public List<Parcela> Parcelas { get; set; } = [];
}