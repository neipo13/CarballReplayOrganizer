using CarballReplayOrganizer;
using CarballReplayOrganizer.FileOperations;
using CarballReplayOrganizer.ReplayParsing;
using CarballReplayOrganizer.Settings;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

internal class Program
{

    private static async Task Main(string[] args)
    {
        //start parsing
        var settings = UpdateSettingsFromArgs(args);
        Console.WriteLine($"Scanning folder: {settings.ReplayFolderPath}");
        if (!Directory.Exists(settings.ReplayFolderPath))
        {
            Console.WriteLine($"Replay folder not found: {settings.ReplayFolderPath}");
            return;
        }
        var parseTimer = Stopwatch.StartNew();
        var parsingTask = ParseReplaysAsync(settings.ReplayFolderPath, settings, debugOutput: false);
        await parsingTask;
        parseTimer.Stop();
        Console.WriteLine();
        Console.WriteLine($"Parsing completed in {parseTimer.Elapsed.TotalSeconds} seconds");
        Console.WriteLine();
        Console.WriteLine();
        ConsoleUtility.DisplayGroupedReplays(GlobalState.ReplayMetas);

        var toMove = GlobalState.ReplayMetas;
        Console.WriteLine($"Ready to move {toMove.Count} replay files from {settings.ReplayFolderPath} to {settings.DestinationFolderPathBase} mode folders");
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();

        Console.WriteLine($"Moving {toMove.Count()} replays...");
        foreach (var replay in toMove)
        {
            //construct destination folder as DestinationFolderPathBase\Mode\filename
            string destFolder = Path.Combine(settings.DestinationFolderPathBase, replay.Mode);
            string destFile = Path.Combine(destFolder, Path.GetFileName(replay.FilePath));
            if (FileUtility.MoveReplayFile(replay.FilePath, destFile))
            {
                Console.WriteLine($"Moved: {replay.Title}");
            }
        }

        Console.WriteLine("Operation complete.");
    }


    /// <summary>
    /// Parses command-line arguments to fill a Settings object.
    /// Expected arguments:
    /// --ReplayFolderPath="path"
    /// --DestinationFolderPathBase="path"
    /// --RankedOnly
    /// --MainModesOnly
    /// --ListOnly
    /// </summary>
    static Settings UpdateSettingsFromArgs(string[] args)
    {
        // Default settings
        var settings = SettingsManager.LoadSettings();

        foreach (var arg in args)
        {
            if (arg.StartsWith("--ReplayFolderPath=", StringComparison.OrdinalIgnoreCase))
            {
                settings.ReplayFolderPath = arg.Substring("--ReplayFolderPath=".Length).Trim('"');
            }
            else if (arg.StartsWith("--DestinationFolderPathBase=", StringComparison.OrdinalIgnoreCase))
            {
                settings.DestinationFolderPathBase = arg.Substring("--DestinationFolderPathBase=".Length).Trim('"');
            }
            else if (arg.Equals("--RankedOnly", StringComparison.OrdinalIgnoreCase))
            {
                settings.RankedOnly = true;
            }
            else if (arg.Equals("--MainModesOnly", StringComparison.OrdinalIgnoreCase))
            {
                settings.MainModesOnly = true;
            }
            else if (arg.Equals("--ListOnly", StringComparison.OrdinalIgnoreCase))
            {
                settings.ListOnly = true;
            }
        }
        SettingsManager.SaveSettings(settings);
        return settings;
    }
    static async Task ParseReplaysAsync(string folderPath, Settings settings, bool debugOutput = false)
    {
        var replayFiles = Directory.GetFiles(folderPath, "*.replay", SearchOption.TopDirectoryOnly);//.Take(20).ToArray();
        GlobalState.TotalReplays = replayFiles.Length;
        Console.WriteLine($"Parsing {replayFiles.Length} Replays");
        foreach (var file in replayFiles)
        {
            var replay = ReplayParser.GetReplayMeta(file, rankedOnly: settings.RankedOnly, mainModesOnly: settings.MainModesOnly, debugOutput: debugOutput);
            if (replay != null)
            {
                GlobalState.ReplayMetas.Add(replay);
            }
            GlobalState.ParsedCount++;
            ConsoleUtility.DrawProgressBar(GlobalState.ParsedCount, GlobalState.TotalReplays);
        }
        GlobalState.IsParsing = false;
    }
    
}