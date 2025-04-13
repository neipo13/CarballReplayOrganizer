using RocketLeagueReplayParser.NetworkStream;

namespace CarballReplayOrganizer.ReplayParsing.Properties
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
