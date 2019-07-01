using Shammill.SignalRClientExample.HttpClient;
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
                var input = Console.ReadLine();
                var output = "";

                if (String.IsNullOrEmpty(input))
                    output = signalRClient.Connection.State.ToString();

                if (input.ToLower().StartsWith("addtogroup"))
                {
                    var param = input.GetParameter();
                    signalRClient.AddToSignalRGroup(param);
                }

                if (input.ToLower().StartsWith("createlobby"))
                {
                    //var param = input.GetParameter();
                    var lobbyId = HttpClients.Post();
                    if (!String.IsNullOrEmpty(lobbyId))
                    {
                        output = $"Created Lobby. Id: {lobbyId}";
                        signalRClient.AddToSignalRGroup(lobbyId);
                    }
                        
                    else output = "Failed To Create Lobby";
                }

                if (input.ToLower().StartsWith("deletelobby"))
                {
                    var param = input.GetParameter();
                    var success = HttpClients.Delete(new Guid(param));
                    if (success) output = "Successfully Deleted Lobby";
                    else output = "Failed to Delete Lobby";
                }

                Console.WriteLine(output);
            }
        }

        public static string GetParameter(this string input)
        {
            var command = input.Split(" ");

            if (command.Length >= 2)
                return command[1];
            else return "";
        }
    }
}
