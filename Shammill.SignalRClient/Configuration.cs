using System;
using System.Collections.Generic;
using System.Text;

namespace Shammill.SignalRClientExample
{
    public static class Configuration
    {
        public static string SignalRUrl = "http://localhost:60742/signalr/";
        public static string ApiUrl = "http://localhost:60742/api/";

        public static Guid PlayerId = new Guid("1114fe29-4724-4194-87ce-37baeb58a38d");
    }
}
