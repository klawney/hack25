namespace Core.Interfaces;

public interface IMessageTrackerService
{
    /// <summary>
    /// Tenta marcar um ID de mensagem como "em processamento".
    /// </summary>
    /// <param name="messageId">O ID único da mensagem/simulação.</param>
    /// <returns>Retorna 'true' se a marcação for bem-sucedida (primeira vez),
    /// e 'false' se a mensagem já estiver marcada.</returns>
    bool TryMarkAsProcessing(long messageId);

    /// <summary>
    /// Remove a marcação de uma mensagem, permitindo que ela seja processada novamente.
    /// </summary>
    /// <param name="messageId">O ID único da mensagem/simulação.</param>
    void MarkAsCompleted(long messageId);
}