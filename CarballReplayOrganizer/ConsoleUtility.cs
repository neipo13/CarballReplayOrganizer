using CarballReplayOrganizer.ReplayParsing;
using RocketLeagueReplayParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarballReplayOrganizer
{
    public static class ConsoleUtility
    {
        /// <summary>
        /// Draws a text-based progress bar in the console.
        /// </summary>
        public static void DrawProgressBar(int progress, int total)
        {
            int barSize = 50;
            double percentage = (double)progress / total;
            int chars = (int)(percentage * barSize);
            Console.CursorLeft = 0;
            Console.Write("[");
            Console.Write(new string('#', chars));
            Console.Write(new string('-', barSize - chars));
            Console.Write($"] {percentage:P0}");
            Console.Write($" {progress}/{total}");
        }

        /// <summary>
        /// Groups and displays the replay metadata by Mode.
        /// </summary>
        public static void DisplayGroupedReplays(List<ReplayMeta> replayMetas)
        {
            var groups = replayMetas.GroupBy(r => r.Mode);

            foreach (var group in groups)
            {
                Console.WriteLine($"Mode: {group.Key}");
                // Print table header with fixed-width columns.
                Console.WriteLine("{0,-50} | {1,-50} | {2,-20}", "FileName", "Title", "Date");
                Console.WriteLine(new string('-', 125));

                foreach (var replay in group)
                {
                    string fileName = Path.GetFileName(replay.FilePath);
                    string title = replay.Title;
                    string date = replay.Date.ToString("g"); // "g" for general date/time
                    Console.WriteLine("{0,-50} | {1,-50} | {2,-20}", fileName, title, date);
                }
                Console.WriteLine();
            }

        }
    }
}
