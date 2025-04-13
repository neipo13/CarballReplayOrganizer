using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarballReplayOrganizer.Settings
{
    public class Settings
    {
        public readonly string defaultReplayFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "My Games", "Rocket League", "TAGame", "DemosEpic");

        public string ReplayFolderPath { get; set; }

        public Settings() 
        {
            ReplayFolderPath = defaultReplayFolder;
        }
    }
}
