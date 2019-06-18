using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Shammill.SignalRClientExample.Models;

namespace Shammill.SignalRClientExample.SignalR
{
    public class SignalRClient
    {
        public HubConnection Connection { get; set; } = null;
        public string UserId { get; set; } = "";

        public SignalRClient()
        {
            CreateSignalRClient();
            CreateSignalRClientEventHandlers();
            ConnectSignalRClientToHub();
        }

        public SignalRClient(string userId)
        {
            this.UserId = userId;
            CreateSignalRClient();
            CreateSignalRClientEventHandlers();
            ConnectSignalRClientToHub();
        }

        public void CreateSignalRClient()
        {
            Connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:60742/signalr", options => { })
                .Build();
        }

        public void CreateSignalRClientEventHandlers()
        {
            Connection.Closed += async (error) =>
            {
                Console.WriteLine("Connection to SignalR Hub closed.");

                await Task.Delay(new Random().Next(0, 5) * 1000);
                await Connection.StartAsync();
            };

            Connection.On<HubMessage>("LobbyUpdated", (message) =>
            {
                Console.WriteLine("Got Lobby Updated Message From SignalR Hub");
                Console.WriteLine($"{message}");
            });

            Connection.On<HubMessage>("LobbyRemoved", (message) =>
            {
                Console.WriteLine("Got Lobby Removed Message From SignalR Hub");
                Console.WriteLine($"{message}");
            });

            Connection.On<HubMessage>("PlayerAddedToLobby", (message) =>
            {
                Console.WriteLine("Got Player Added To Lobby Message From SignalR Hub");
                Console.WriteLine($"{message}");
            });

            Connection.On<HubMessage>("PlayerRemovedFromLobby", (message) =>
            {
                Console.WriteLine("Got Player Removed From Lobby Message From SignalR Hub");
                Console.WriteLine($"{message}");
            });

            Connection.On<string>("Connected", (connectionId) =>
            {
                Console.WriteLine("Connected To Hub, Connection Id:");
                Console.WriteLine($"{connectionId}");
                Connection.SendAsync("AddToGroup", UserId);
            });

            Connection.On<string>("AddedToGroup", (message) =>
            {
                Console.WriteLine($"I was added to group {message}");
            });
        }

        public void ConnectSignalRClientToHub()
        {
            Console.WriteLine($"State: { Connection.State}");
            Console.WriteLine($"Start connecting to hub.");
            Connection.StartAsync().ContinueWith(x => Console.WriteLine($"Connection Start Result: {Connection.State}"));
        }
    }
}
