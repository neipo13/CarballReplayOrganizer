using RocketLeagueReplayParser.NetworkStream;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetReplayParser.CarDefinitions
{
    public enum PropertyType
    {
        Player,
        Car
    }
    public class PropertyWrapper
    {
        public PropertyType Type { get; set; }
        public ActorState State { get; set; }
        public Frame Frame { get; set; }
        public ActorStateProperty Property { get; set; }
    }
}
