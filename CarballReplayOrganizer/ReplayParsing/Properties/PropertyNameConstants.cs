using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarballReplayOrganizer.ReplayParsing.Properties
{
    public static class PropertyNameConstants
    {
        public const string PLAYER_NAME_ENGINE = "Engine.PlayerReplicationInfo:PlayerName";
        public const string CAR_INFO_ENGINE = "Engine.Pawn:PlayerReplicationInfo";
        public const string TEAM_PAINT_ENGINE = "TAGame.Car_TA:TeamPaint";
        public const string LOADOUT_ENGINE = "TAGame.PRI_TA:ClientLoadouts";
        public const string LOADOUT_PAINTS_ENGINE = "TAGame.PRI_TA:ClientLoadoutsOnline";


        public const string PLAYER_STATS_PROPERTY = "PlayerStats";
        public const string PLAYER_NAME_PROPERTY = "Name";
        public const string PLAYER_TEAM_PROPERTY = "Team";
    }
}
