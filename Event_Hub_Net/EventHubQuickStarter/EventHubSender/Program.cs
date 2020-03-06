using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using System;
using Azure.Storage.Blobs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;

namespace EventHubSender
{
    class Program
    {
        
        static async Task Main(string[] args)
        {

            
        
            //Setando as variaveis de ambiente
            System.Environment.SetEnvironmentVariable("blobStorageConnectionString","DefaultEndpointsProtocol=https;AccountName=augustofunction;AccountKey=agw1AaIlqCOw7SgC0mYRW51GQvbCgfCUJyB8upSKlDoaTDIET1T6JZVU41HcGno7FjDYqNpxaTIJxMbe8zETrw==;EndpointSuffix=core.windows.net");
            System.Environment.SetEnvironmentVariable("blobContainerName","eventhubexample");
            System.Environment.SetEnvironmentVariable("connectionStringHub", "Endpoint=sb://myenvent.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=hnuuk+GaadZOth+bnuJoY/AOYB18XBpR+FR6sKDAC3I=");
            System.Environment.SetEnvironmentVariable("eventHub","mainhub");

            String Option = "";
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("Pressione 1 para escrever no EventHub");
            Console.WriteLine("Pressione 2 para ler do EventHub");
            Console.WriteLine("----------------------------------------------------------");
            Option = Console.ReadLine().ToString();
            
            
            
            switch(Option)
            {
                case "1":
                    WriteEventHub write = new WriteEventHub();
                    await write.WriteEventHubAsync();
                    break;
                case "2":
                    ReadEventHub read = new ReadEventHub();
                    await read.ReadEventHubMethod();
                    break;

                default:
                    Console.WriteLine(" ");
                    Console.WriteLine("Opção Invalida");
                    break;

            }
        }
    }
}
