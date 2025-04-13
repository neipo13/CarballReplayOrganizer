using RocketLeagueReplayParser.NetworkStream;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetReplayParser.CarDefinitions
{
    public class Player
    {
        public string PlayerName { get; set; }
        public string UniqueID { get; set; }
        public bool isOrange { get; set; } = false;
        public List<uint> ActorIDs { get; set; }
        public List<Car> AttachedCars { get; set; } = new List<Car>();


        public ClientLoadouts Loadout { get; set; }
        public ClientLoadoutsOnline LoadoutPaints { get; set; }

        public Color MainColor
        {
            get
            {
                if (AttachedCars == null || AttachedCars.Count == 0) return Color.White;
                var mainLookup = isOrange ? RocketLeagueColors.Orange : RocketLeagueColors.Blue;
                var colorArray = mainLookup[AttachedCars.First().TeamPaint.TeamColorId];
                return Color.FromArgb(colorArray[0], colorArray[1], colorArray[2]);
            }
        }
        public Color AccentColor
        {
            get
            {
                if (AttachedCars == null || AttachedCars.Count == 0) return Color.White;
                var mainLookup = RocketLeagueColors.Accent;
                var colorArray = mainLookup[AttachedCars.First().TeamPaint.CustomColorId];
                return Color.FromArgb(colorArray[0], colorArray[1], colorArray[2]);
            }
        }

        public string MainColorLocation
        {
            get
            {
                if (AttachedCars == null || AttachedCars.Count == 0) return "1 : 1";
                var i = AttachedCars.First().TeamPaint.TeamColorId;
                var row = i % 10;
                var col = i / 10;

                return $"{row} : {col}";
            }
        }
        public string AccentColorLocation
        {
            get
            {
                if (AttachedCars == null || AttachedCars.Count == 0) return "1 : 1";
                var i = AttachedCars.First().TeamPaint.CustomColorId;
                var col = i % 15;
                var row = i / 15;

                return $"{row} : {col}";
            }
        }
    }
}
