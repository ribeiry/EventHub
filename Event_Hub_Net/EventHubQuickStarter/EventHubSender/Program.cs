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
            System.Environment.SetEnvironmentVariable("blobStorageConnectionString","");
            System.Environment.SetEnvironmentVariable("blobContainerName","");
            System.Environment.SetEnvironmentVariable("connectionStringHub", "");
            System.Environment.SetEnvironmentVariable("eventHub","");

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
