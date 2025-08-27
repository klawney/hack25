using Azure.Messaging.EventHubs.Producer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Mensageria.Extensions
{
    public static class EventHubExtensions
    {
        public static IServiceCollection AddEventHubProducer(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["AzureEventHub:ConnectionString"];
            var eventHubName = config["AzureEventHub:HubName"]; 
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), 
                    "A chave de configuração 'AzureEventHub:ConnectionString' não foi encontrada ou não possui um valor. Verifique sua fonte de configuração.");
            }
            
            // É uma boa prática garantir que o HubName também está configurado,
            // mesmo que não seja usado diretamente neste construtor, para
            // evitar confusão futura.
            if (string.IsNullOrEmpty(eventHubName))
            {
                throw new ArgumentNullException(nameof(eventHubName), 
                    "A chave de configuração 'AzureEventHub:HubName' não foi encontrada ou não possui um valor. Verifique sua fonte de configuração.");
            }

            services.AddSingleton(sp => new EventHubProducerClient(connectionString));
            
            return services;
        }
    }
}