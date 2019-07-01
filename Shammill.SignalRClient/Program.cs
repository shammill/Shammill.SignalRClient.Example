using Shammill.SignalRClientExample.SignalR;
using Shammill.SignalRClientExample.Input;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Shammill.SignalRClientExample
{
    class Program
    {
        static SignalRClient signalRClient;
        static bool startUp = true;


        static void Main(string[] args)
        {
            StartUp();

            InputHandler.HandleInput(signalRClient);
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
