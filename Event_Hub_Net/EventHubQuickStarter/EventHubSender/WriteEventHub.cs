using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventHubSender
{
    public class WriteEventHub
    {
       
        public async Task WriteEventHubAsync() {

            var connectionString = System.Environment.GetEnvironmentVariable("connectionStringHub");
            var eventHub = System.Environment.GetEnvironmentVariable("eventHub");
            await using(var producerClient = new EventHubProducerClient(connectionString, eventHub))
            {
               using EventDataBatch eventDataBatch = await producerClient.CreateBatchAsync();


                eventDataBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes($"My First Event {DateTime.Now}")));
                eventDataBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes($"My Second Event {DateTime.Now}")));
                eventDataBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes($"My third Event {DateTime.Now}")));

                await producerClient.SendAsync(eventDataBatch);
                Console.WriteLine("O batch 3 eventos publicados");
            }
        }
    }
}
