namespace Core.Exceptions;

public class ProdutoException : DomainException
{
    private ProdutoException(string message) : base(message)
    {
    }
    public static ProdutoException ProdutoInexistente()
        => new ProdutoException("Nenhum produto foi encontrado com os critérios especificados.");

    public static ProdutoException ProdutoInexistente(decimal valorSolicitado, int prazoSolicitado)
        => new ProdutoException($"Nenhum produto foi encontrado para o valor: {valorSolicitado:C} ou prazo de: {prazoSolicitado:D} meses");
}
