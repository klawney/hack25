// using System.Reflection;
// using Core.Events;
// using Infra.Persistence;
// using MassTransit;
// using MassTransit.EntityFrameworkCore;

// namespace Api.Extentions;

// public static class MessagingInfrastructureExtensions
// {
//     public static void AddMessagingInfrastructure(this IServiceCollection services, IConfiguration configuration)
//     {
//         services.AddMassTransit(busConfigurator =>
//         {
//             busConfigurator.AddConsumers(Assembly.GetExecutingAssembly());
//             busConfigurator.AddEntityFrameworkOutbox<SimulacaoDbContext>();

//             busConfigurator.UsingInMemory((context, inMemoryConfigurator) =>
//             {
//                 inMemoryConfigurator.ConfigureEndpoints(context);
//             });

//             // ABORDAGEM REVISADA E MAIS ROBUSTA PARA O RIDER
//             busConfigurator.AddRider(riderConfigurator =>
//             {
//                 // Configuração explícita do transporte do Rider
//                 // riderConfigurator.UsingEventHub( (context, eventHubConfigurator) =>
//                 // {
//                 //     eventHubConfigurator.Host(configuration.GetConnectionString("EventHubConnection"));
//                 // });
//             });

//             // Configuração explícita do roteamento da mensagem
//             // fora da configuração do Rider
//             busConfigurator.AddRequestClient<SimulacaoGeradaParaEventHub>();
//             EndpointConvention.Map<SimulacaoGeradaParaEventHub>(new Uri($"topic:{configuration["EventHubName"]}"));
//         });
//     }
// }