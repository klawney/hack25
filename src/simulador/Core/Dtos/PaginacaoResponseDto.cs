namespace Core.Dtos;
public record PaginacaoResponseDto
(
    int Pagina,
    int QtdRegistros,
    int QtdRegistrosPagina,
    List<SimulacaoRegistroDto> Registros
);