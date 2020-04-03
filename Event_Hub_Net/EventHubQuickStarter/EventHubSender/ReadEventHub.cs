using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Messaging.EventHubs.Producer;
using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EventHubSender
{

    class ReadEventHub
    {
        private static readonly string blobStorageConnectionString = System.Environment.GetEnvironmentVariable("blobStorageConnectionString");
        private static readonly string blobContainerName = System.Environment.GetEnvironmentVariable("blobContainerName");
        public static List<string> msg = new List<string>();

        /// <summary>
        /// Função quer ira Ler do EventHub
        /// </summary>
        /// <returns></returns>
        public async Task ReadEventHubMethod()
        {

            var connectionString = System.Environment.GetEnvironmentVariable("connectionStringHub");
            var eventHub = System.Environment.GetEnvironmentVariable("eventHub");
            

        await using (var producerClient = new EventHubProducerClient(connectionString, eventHub))
            {
                string consumeGroup = EventHubConsumerClient.DefaultConsumerGroupName;
                

                //Criando a conexão de storage
                BlobContainerClient storageClient = new BlobContainerClient(blobStorageConnectionString, blobContainerName);

                //Criando a conexão com o eventProcessor
                EventProcessorClient eventProcessor = new EventProcessorClient(storageClient, consumeGroup, connectionString, eventHub);

                //Registro de Erro de processo
                eventProcessor.ProcessEventAsync += ProcessEventHandler;
                eventProcessor.ProcessErrorAsync += ProcessErrorHandler;


                //Iniciando o processo de leitura
                await eventProcessor.StartProcessingAsync();

                //segurando a task por 10 segundos para o processo iniciar
                await Task.Delay(TimeSpan.FromSeconds(10));

                //Enviando para o container blob
                if(msg.Count > 0) { SaveBlob(msg); }
                

                //Parando o processo
                await eventProcessor.StopProcessingAsync();


            }
            static Task ProcessEventHandler(ProcessEventArgs eventArgs)
            {

                Console.WriteLine("\t Reicived event: {0}", Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray()));
                
                msg.Add(Encoding.Default.GetString(eventArgs.Data.Body.ToArray()));
                return Task.CompletedTask;
            }

            static Task ProcessErrorHandler(ProcessErrorEventArgs eventArgs)
            {
                Console.WriteLine($"\tPartition '{ eventArgs.PartitionId}': an  an unhandled exception was encountered. This was not expected to happen.");
                Console.WriteLine(eventArgs.Exception.Message);

                return Task.CompletedTask;

            }
        }
        /// <summary>
        /// Função para salvar no Container os Logs
        /// </summary>
        /// <param name="texto">Parametro com a informação ser salvo</param>
        static async void SaveBlob(List<String> texto)
        {
            String storageConnection = "StorageConnectionContainer";
            BlobServiceClient blobServiceClient = new BlobServiceClient(storageConnection);

            BlobContainerClient containerClient = new BlobContainerClient(storageConnection, "cointanerNameLog");


            _ = containerClient.CreateIfNotExists();


            string localPath = "c:\\EventHub\\";
            Directory.CreateDirectory(localPath);
            string filename = "quickstart" + Guid.NewGuid().ToString() + ".log";
            string localFileName = Path.Combine(localPath, filename);


            await File.WriteAllLinesAsync(localFileName, texto);
            

            BlobClient blobClient = containerClient.GetBlobClient(filename);

            Console.WriteLine("Uploading to blobStorage: \n \t{0}\n", blobClient.Uri);

            using FileStream uploadFileSteam = File.OpenRead(localFileName);
            await blobClient.UploadAsync(uploadFileSteam, true);
            uploadFileSteam.Close();
        }

    }

}
