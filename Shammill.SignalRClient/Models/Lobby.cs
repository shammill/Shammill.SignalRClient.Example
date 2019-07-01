using System;
using System.Collections.Generic;
using System.Text;

namespace Shammill.SignalRClientExample.Models
{
    public class Lobby
    {
        public Guid Id;
        public string Name;
        public List<Player> Players;

        public int MaximumSize;
        public bool IsPublic;
        public bool IsJoinable;
        public bool HasGameInProgress;
        public int GameId;

        // useful for filters
        public RegionEnum Region;
        public string GameMode;
    }
}
