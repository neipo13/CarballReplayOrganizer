using CarballReplayOrganizer.Settings;
using System;
using System.IO;
using System.Text.Json;

namespace CarballReplayOrganizer.Settings
{
    public static class SettingsManager
    {
        private static readonly string SettingsFilePath = "settings.json";

        public static Settings LoadSettings()
        {
            if (File.Exists(SettingsFilePath))
            {
                string json = File.ReadAllText(SettingsFilePath);
                return JsonSerializer.Deserialize<Settings>(json);
            }
            else
            {
                // Create default settings and save to file if it doesn't exist
                var settings = new Settings();
                SaveSettings(settings);
                return settings;
            }
        }

        public static void SaveSettings(Settings settings)
        {
            var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SettingsFilePath, json);
        }
    }


}