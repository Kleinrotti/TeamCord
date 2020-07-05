using Newtonsoft.Json;
using System;
using System.IO;

namespace TeamCord.Core
{
    public class DataStorage
    {
        private static string _configPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TS3Client\plugins\TeamCord\";

        public DataStorage()
        {
            Directory.CreateDirectory(_configPath);
            if (!File.Exists(_configPath + @"config.json"))
            {
                Logging.Log("Config file does not exists, creating one...");
                StoreSettings(new PluginSettings());
            }
        }

        public void StoreSettings(PluginSettings settings)
        {
            Logging.Log("Storing plugin settings...", LogLevel.LogLevel_INFO);
            var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(_configPath + @"config.json", json);
        }

        public PluginSettings GetSettings()
        {
            Logging.Log("Reading plugin settings...", LogLevel.LogLevel_INFO);
            var jsonString = File.ReadAllText(_configPath + @"config.json");
            return JsonConvert.DeserializeObject<PluginSettings>(jsonString);
        }
    }
}