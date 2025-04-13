using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarballReplayOrganizer.ReplayParsing
{
    public class ReplayMeta
    {
        public string Title { get; set; }
        public string Mode { get; set; }
        public DateTime Date { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public bool IsRanked { get; set; }  
    }

}
