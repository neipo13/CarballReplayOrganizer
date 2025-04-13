using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarballReplayOrganizer.ReplayParsing.Properties
{
    public class Player
    {
        public string PlayerName { get; set; }
        public string UniqueID { get; set; }
        public List<uint> ActorIDs { get; set; }
        public bool isOrange { get; set; } = false;
    }
}
