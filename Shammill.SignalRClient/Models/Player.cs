using System;
using System.Collections.Generic;
using System.Text;

namespace Shammill.SignalRClientExample.Models
{
    public class Player
    {
        public Guid Id;
        public string ConnectionId;
        public string Name;
        public bool IsLobbyLeader;
    }
}
