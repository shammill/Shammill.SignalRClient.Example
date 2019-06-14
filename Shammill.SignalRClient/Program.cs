using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace Shammill.SignalRClient
{
    class Program
    {
        private static HubConnection connection;
        static bool startUp = true;
        static string userId = "1114fe29-4724-4194-87ce-37baeb58a38d";


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
                .WithUrl("http://localhost:60742/signalr", options =>
                {
                    //options.AccessTokenProvider = () => Task.FromResult(userId),
                    options.Credentials = new Credential();
                })
                .Build();
        }

        public static void CreateSignalRClientEventHandlers()
        {
            connection.Closed += async (error) =>
            {
                Console.WriteLine("Connection to SignalR Hub closed.");

                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            connection.On<HubMessage>("LobbyUpdated", (message) =>
            {
                Console.WriteLine("Got Lobby Updated Message From SignalR Hub");
                Console.WriteLine($"{message}");
            });

            connection.On<HubMessage>("LobbyRemoved", (message) =>
            {
                Console.WriteLine("Got Lobby Removed Message From SignalR Hub");
                Console.WriteLine($"{message}");
            });

            connection.On<HubMessage>("PlayerAddedToLobby", (message) =>
            {
                Console.WriteLine("Got Player Added To Lobby Message From SignalR Hub");
                Console.WriteLine($"{message}");
            });

            connection.On<HubMessage>("PlayerRemovedFromLobby", (message) =>
            {
                Console.WriteLine("Got Player Removed From Lobby Message From SignalR Hub");
                Console.WriteLine($"{message}");
            });

            connection.On<string>("Connected", (connectionId) =>
            {
                Console.WriteLine("Connected To Hub, Connection Id:");
                Console.WriteLine($"{connectionId}");
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
            public object content;
        }

        public class Credential : ICredentials
        {
            public NetworkCredential GetCredential(Uri uri, string authType) {
                return new NetworkCredential {
                    UserName = userId,
                    Domain = "localhost"
                };
            }
        }
    }
}
