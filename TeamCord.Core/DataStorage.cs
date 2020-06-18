using Newtonsoft.Json;
using System;
using System.IO;

namespace TeamCord.Core
{
    public class DataStorage
    {
        private string _configPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Teamcord\config\config.json";

        public void StoreSettings(PluginSettings settings)
        {
            Logging.Log("Storing plugin settings...", LogLevel.LogLevel_INFO);
            var json = JsonConvert.SerializeObject(settings);
            File.WriteAllText(_configPath, json);
        }

        public PluginSettings GetSettings()
        {
            Logging.Log("Reading plugin settings...", LogLevel.LogLevel_INFO);
            var jsonString = File.ReadAllText(_configPath);
            return JsonConvert.DeserializeObject<PluginSettings>(jsonString);
        }
    }
}