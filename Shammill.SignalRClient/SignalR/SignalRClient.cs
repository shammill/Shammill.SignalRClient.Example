﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Shammill.SignalRClientExample.Models;

namespace Shammill.SignalRClientExample.SignalR
{
    public class SignalRClient
    {
        public static class ConnectionDetails
        {
            public static string ConnectionId { get; set;}
            public static string UserId { get; set;}
        }

        public HubConnection Connection { get; set; } = null;
        public string UserId { get; set; } = "354d9e59-198e-485f-bd66-c58bb4978a0e"; // Guid.NewGuid().ToString();

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
                .WithUrl(Configuration.SignalRUrl, options => { })
                .Build();
        }

        public void CreateSignalRClientEventHandlers()
        {
            Connection.Closed += async (error) =>
            {
                ConnectionDetails.ConnectionId = null;
                Console.WriteLine("Connection to SignalR Hub closed.");

                while (Connection.State == HubConnectionState.Disconnected)
                {
                    Console.WriteLine("Attempting reconnect to SignalR Hub");
                    await Task.Delay(new Random().Next(3, 6) * 1000);
                    await Connection.StartAsync().ContinueWith(x => Console.WriteLine($"Connection Start Result: {Connection.State}"));
                }
            };

            Connection.On<Lobby>("LobbyCreated", (message) =>
            {
                Console.WriteLine("Lobby Created Message From SignalR Hub");
                Console.WriteLine($"{message.Id.ToString()}");
            });

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

            Connection.On<Guid>("LobbyDeleted", (message) =>
            {
                Console.WriteLine("Got Lobby Deleted Message From SignalR Hub");
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
                ConnectionDetails.ConnectionId = connectionId;
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


        public void AddToSignalRGroup(string group)
        {
            Connection.SendAsync("AddToGroup", group);
        }

        public void CreateLobby()
        {
            Lobby lobby = new Lobby();
            lobby.Name = "My New Lobby";
            lobby.Region = RegionEnum.Australia;
            Connection.SendAsync("CreateLobby", lobby);
        }

        public void DeleteLobby(Guid lobbyId)
        {
            Connection.SendAsync("DeleteLobby", lobbyId);
        }
    }
}
