using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace Shammill.SignalRClient
{
    class Program
    {
        private static HubConnection connection;
        static bool startUp = true;


        // Very Basic SignalR Client Example
        static void Main(string[] args)
        {
            if (startUp == true)
                StartUp();

            while (true)
            {
                Console.ReadKey();
                Console.WriteLine($"State: {connection.State}");
            }
        }

        public static void StartUp()
        {
            if (startUp)
            {
                CreateSignalRClient();
                CreateSignalRClientEventHandlers();
                ConnectSignalRClientToHub();

                startUp = false;
            }
        }

        public static void CreateSignalRClient()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:60742/signalr")
                .Build();
        }

        public static void CreateSignalRClientEventHandlers()
        {
            connection.Closed += (error) =>
            {
                Console.WriteLine("Connection to SignalR Hub closed.");
                return Task.CompletedTask;
            };

            connection.On<object>("ReceiveMessage", (message) =>
            {
                Console.WriteLine("Got Message From SignalR Hub");
                Console.WriteLine($"{message}");
            });
        }

        public static void ConnectSignalRClientToHub()
        {
            Console.WriteLine($"State: { connection.State}");
            Console.WriteLine($"Start connecting to hub.");
            connection.StartAsync().ContinueWith(x => Console.WriteLine($"Connection Start Result: {connection.State}"));
        }




        public class HubMessage
        {
            public string context;
            public object message;
        }
    }
}
