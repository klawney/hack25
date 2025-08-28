using System.Collections.Concurrent;
using Core.Interfaces;

namespace Api.Services; // Ou o namespace apropriado

public class InMemoryMessageTrackerService : IMessageTrackerService
{
    // Dicionário thread-safe para armazenar IDs em processamento.
    // A chave é o ID da simulação, o valor (bool) é apenas um placeholder.
    private readonly ConcurrentDictionary<long, bool> _processingMessages = new();

    public bool TryMarkAsProcessing(long messageId)
    {
        // O método TryAdd é atômico. Ele retorna true se a chave foi
        // adicionada com sucesso (não existia antes), e false se a
        // chave já existe. É exatamente o que precisamos.
        return _processingMessages.TryAdd(messageId, true);
    }

    public void MarkAsCompleted(long messageId)
    {
        // Remove a chave do dicionário, permitindo que uma futura
        // requisição com o mesmo ID (se aplicável) seja processada.
        _processingMessages.TryRemove(messageId, out _);
    }
}