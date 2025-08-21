using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mensageria.Extensions
{
    public static class EventHubExtensions
    {
        public static IServiceCollection AddEventHubProducer(this IServiceCollection services, IConfiguration config, string connectionName = "EventHubConnection", string eventHubNameKey = "EventHubName")
        {
            var connectionString = config.GetConnectionString(connectionName);
            var eventHubName = config[eventHubNameKey];
            services.AddSingleton(sp => new EventHubProducerClient(connectionString, eventHubName));
            return services;
        }
    }
}
