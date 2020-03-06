using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Messaging.EventHubs.Producer;
using Azure.Storage.Blobs;
using System;
using System.Text;
using System.Threading.Tasks;

namespace EventHubSender
{

    class ReadEventHub
    {
        
        public async Task ReadEventHubMethod()
        {

        var connectionString = System.Environment.GetEnvironmentVariable("connectionStringHub");
        var eventHub = System.Environment.GetEnvironmentVariable("eventHub");
        var blobStorageConnectionString = System.Environment.GetEnvironmentVariable("blobStorageConnectionString");
        var blobContainerName = System.Environment.GetEnvironmentVariable("blobContainerName");

        await using (var producerClient = new EventHubProducerClient(connectionString,eventHub ))
        {
                string consumeGroup = EventHubConsumerClient.DefaultConsumerGroupName;

                BlobContainerClient storageClient = new BlobContainerClient(blobStorageConnectionString,blobContainerName);


                EventProcessorClient eventProcessor = new EventProcessorClient(storageClient, consumeGroup, connectionString, eventHub);

                eventProcessor.ProcessEventAsync += ProcessEventHandler;
                eventProcessor.ProcessErrorAsync += ProcessErrorHandler;

                await eventProcessor.StartProcessingAsync();

                await Task.Delay(TimeSpan.FromSeconds(10));

                await eventProcessor.StopProcessingAsync();


            }

            static Task ProcessEventHandler(ProcessEventArgs eventArgs)
            {
                Console.WriteLine("\t Reicived event: {0}", Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray()));

                return Task.CompletedTask;
            }

            static Task ProcessErrorHandler(ProcessErrorEventArgs eventArgs)
            {
                Console.WriteLine($"\tPartition '{ eventArgs.PartitionId}': an  an unhandled exception was encountered. This was not expected to happen.");
                Console.WriteLine(eventArgs.Exception.Message);

                return Task.CompletedTask;


            }

        }
    }
}
