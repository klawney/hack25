using Xunit;
using Azure.Messaging.EventHubs.Producer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Threading.Tasks;

namespace Mensageria.Tests
{
    public class EventHubSendMessageTests
    {
        // [Fact(Skip = "Test requires a real Azure Event Hub connection string and will send a real message.")]

        [Fact]
        public async Task SendMessageToEventHub_Works()
        {
            // Arrange
            var inMemorySettings = new Dictionary<string, string> {
                {"ConnectionStrings:EventHubConnection", "<SUA_CONNECTION_STRING_AQUI>"},
                {"EventHubName", "<SEU_EVENT_HUB_NAME_AQUI>"}
            };
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            var services = new ServiceCollection();
            services.AddEventHubProducer(configuration);
            var provider = services.BuildServiceProvider();
            var client = provider.GetRequiredService<EventHubProducerClient>();

            // Act
            using var eventBatch = await client.CreateBatchAsync();
            eventBatch.TryAdd(new Azure.Messaging.EventHubs.EventData(Encoding.UTF8.GetBytes("Mensagem de teste do xUnit")));
            await client.SendAsync(eventBatch);

            // Assert
            // Se não lançar exceção, consideramos sucesso
            Assert.True(true);
        }
    }
}
