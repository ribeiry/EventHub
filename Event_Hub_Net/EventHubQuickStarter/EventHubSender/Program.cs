using System;
using System.Threading.Tasks;

namespace EventHubSender
{
    class Program
    {

        static async Task Main(string[] args)
        {

            //Setando as variaveis de ambiente
            System.Environment.SetEnvironmentVariable("blobStorageConnectionString", "connectionString Container ErrorLog");
            System.Environment.SetEnvironmentVariable("blobContainerName", "container name ErrorLog");
            System.Environment.SetEnvironmentVariable("connectionStringHub", "connectionString EventHub");
            System.Environment.SetEnvironmentVariable("eventHub", "EventHubTopic");
            Boolean exit = true;
            while (exit)
            {
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine("Pressione 1 para escrever no EventHub");
                Console.WriteLine("Pressione 2 para ler do EventHub");
                Console.WriteLine("Pressione 3 para sair");
                Console.WriteLine("----------------------------------------------------------");
                string Option = Console.ReadLine().ToString();

                switch (Option)
                {
                    case "1":
                        WriteEventHub write = new WriteEventHub();
                        await write.WriteEventHubAsync();
                        break;
                    case "2":
                        ReadEventHub read = new ReadEventHub();
                        await read.ReadEventHubMethod();
                        break;
                    case "3":
                        exit = false;
                        break;
                    default:
                        Console.WriteLine(" ");
                        Console.WriteLine("Opção Invalida");
                        break;

                }
            }
        }
    }
}
