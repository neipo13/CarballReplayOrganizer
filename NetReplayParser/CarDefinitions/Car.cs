using RocketLeagueReplayParser.NetworkStream;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetReplayParser.CarDefinitions
{
    public class Car
    {
        public uint Id { get; set; }
        public uint PlayerActorId { get; set; }
        public TeamPaint? TeamPaint { get; set; }
    }
}
