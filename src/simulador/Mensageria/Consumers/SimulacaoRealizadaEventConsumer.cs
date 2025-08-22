using Core.Events;
using MassTransit;

namespace Mensageria.Consumers
{
    public class SimulacaoRealizadaEventConsumer : IConsumer<SimulacaoRealizadaEvent>
    {
        private readonly EventHubSimulacaoProducer _producerEhub;
        public SimulacaoRealizadaEventConsumer(EventHubSimulacaoProducer producer)
        {
            _producerEhub = producer;
        }

        public async Task Consume(ConsumeContext<SimulacaoRealizadaEvent> context)
        {
            if (context.Message.Simulacao != null)
                await _producerEhub.EnviarSimulacaoAsync(context.Message.Simulacao);
        }
    }
}
