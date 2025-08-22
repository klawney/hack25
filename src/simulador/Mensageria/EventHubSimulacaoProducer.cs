using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;

namespace Mensageria
{
	public class EventHubSimulacaoProducer
	{
		private readonly EventHubProducerClient _producerClient;

		public EventHubSimulacaoProducer(EventHubProducerClient producer)
		{
			_producerClient = producer;
		}

		public async Task EnviarSimulacaoAsync<T>(T dto)
		{
			string mensagem = JsonSerializer.Serialize(dto);
			using EventDataBatch batch = await _producerClient.CreateBatchAsync();
			batch.TryAdd(new EventData(Encoding.UTF8.GetBytes(mensagem)));
			await _producerClient.SendAsync(batch);
		}
	}
}
