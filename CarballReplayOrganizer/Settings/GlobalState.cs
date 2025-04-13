using CarballReplayOrganizer.ReplayParsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarballReplayOrganizer.Settings
{
    public static class GlobalState
    {
        public static bool IsParsing = true;
        public static int TotalReplays = 0;
        public static int ParsedCount = 0;
        public static List<ReplayMeta> ReplayMetas = new List<ReplayMeta>();
    }
}
