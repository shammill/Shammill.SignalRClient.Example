using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using Shammill.SignalRClientExample.Models;

namespace Shammill.SignalRClientExample.HttpClient
{
    public static class HttpClients
    {

        public static void GetLobby(Guid lobbyId)
        {
            var client = new RestClient(Configuration.ApiUrl);
            var request = new RestRequest("lobbies?lobbyId=" + lobbyId.ToString(), Method.GET);
            var queryResult = client.Execute<List<Lobby>>(request).Data;
        }

        public static void GetLobbies()
        {
            var client = new RestClient(Configuration.ApiUrl);
            var request = new RestRequest("lobbies", Method.GET);
            var queryResult = client.Execute<List<Lobby>>(request).Data;
        }

        public static string Post()
        {
            var client = new RestClient(Configuration.ApiUrl);
            var request = new RestRequest("lobbies", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new Lobby
            {
                Name = "MyNewLobby",
                Region = RegionEnum.Australia,
                IsPublic = true,
                IsJoinable = true,
                HasGameInProgress = false,
                MaximumSize = 4,
                Players = new List<Player>() { new Player() { Id =  Configuration.PlayerId, Name = "NewPlayer"} }
            });

            IRestResponse<Lobby> response = client.Execute<Lobby>(request);
            Lobby newLobby = Newtonsoft.Json.JsonConvert.DeserializeObject<Lobby>(response.Content);
          
            return newLobby.Id.ToString();
        }

        public static bool Delete(Guid lobbyId)
        {
            var client = new RestClient(Configuration.ApiUrl);
            var request = new RestRequest("lobbies/" + lobbyId.ToString(), Method.DELETE);

            //request.AddParameter("lobbyId", lobbyId);

            var response = client.Execute<bool>(request);
            var success = response.Data;
            return success;
        }

    }
}
