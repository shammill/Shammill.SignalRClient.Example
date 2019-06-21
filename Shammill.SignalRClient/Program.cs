﻿using Shammill.SignalRClientExample.SignalR;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Shammill.SignalRClientExample
{
    class Program
    {
        static SignalRClient signalRClient;
        static bool startUp = true;
        static string userId = "1114fe29-4724-4194-87ce-37baeb58a38d";


        static void Main(string[] args)
        {
            StartUp();

            while (true)
            {
                Console.ReadKey();
                Console.WriteLine($"State: {signalRClient.Connection.State}");
            }
        }

        public static void StartUp()
        {
            if (startUp)
            {
                signalRClient = new SignalRClient();
                startUp = false;
            }
        }


    }
}
