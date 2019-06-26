using Shammill.SignalRClientExample.SignalR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shammill.SignalRClientExample.Input
{
    public static class InputHandler
    {
        public static void HandleInput(SignalRClient signalRClient)
        {
            while (true)
            {
                Console.ReadKey();
                Console.WriteLine($"State: {signalRClient.Connection.State}");
            }
        }
    }
}
