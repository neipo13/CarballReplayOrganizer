using CarballReplayOrganizer.Replay;
using CarballReplayOrganizer.Settings;

var settings = SettingsManager.LoadSettings();
Console.WriteLine($"Scanning folder: {settings.ReplayFolderPath}");


if (Directory.Exists(settings.ReplayFolderPath))
{
    var replayFiles = Directory.GetFiles(settings.ReplayFolderPath, "*.replay", SearchOption.TopDirectoryOnly);
    var replays = new List<ReplayMeta>();
    foreach (var file in replayFiles)
    {
        //parse replay
        var parsed = RocketLeagueReplayParser.Replay.Deserialize(file);
        // For now we simply output file info; you'll replace this stub with your parser.
        replays.Add(new ReplayMeta
        {
            Title = Path.GetFileNameWithoutExtension(file), // Placeholder for actual title extraction
            Mode = "Unknown",  // Placeholder for mode extraction
            Date = File.GetCreationTime(file),
            FilePath = file
        });
        // Output replays
        foreach (var replay in replays)
        {
            Console.WriteLine($"Title: {replay.Title}, Mode: {replay.Mode}, Date: {replay.Date}");
        }
    }

}
else
{
    Console.WriteLine("Replay folder not found. Please update settings.json with a valid folder path.");
}