
namespace Core.Dtos;
public record ResultadoSimulacaoDto
(
    string? Tipo,
    List<ParcelaDto> Parcelas
   // List<ParcelaDto> ParcelasSacPrice
);