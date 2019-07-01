using System;
using System.Collections.Generic;
using System.Text;

namespace Shammill.SignalRClientExample.Models
{
    public class LobbyFilter
    {
        public bool IsPublic;
        public bool HasGameInProgress;

        public bool IsNotFull;

        // useful for filters
        public RegionEnum Region;
        public string GameMode;
    }
}
