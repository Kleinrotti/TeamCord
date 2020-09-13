using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;

namespace TeamCord.Core
{
    /// <summary>
    /// Read and write settings for TeamCord
    /// </summary>
    public class DataStorage
    {
        private static string _configPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TS3Client\plugins\TeamCord\";

        public DataStorage()
        {
            if (!File.Exists(_configPath + @"config.json"))
            {
                Directory.CreateDirectory(_configPath);
                Logging.Log("Config file does not exists, creating one...");
                StoreSettings(new PluginSettings());
            }
            else
            {
                var settings = GetSettings();
                //check if config file is too old and replace it then
                if (settings.ConfigVersion == null || settings.ConfigVersion.CompareTo(PluginSettings.CurrentConfigVersion) < 0)
                {
                    Logging.Log("Old config file detected, replacing with new one...");
                    var newSettings = new PluginSettings();
                    StoreSettings(newSettings);
                    MessageBox.Show("Please check your settings of TeamCord, a new version has been applied.");
                }
            }
        }

        /// <summary>
        /// Save setting to config file
        /// </summary>
        /// <param name="settings"></param>
        public void StoreSettings(PluginSettings settings)
        {
            Logging.Log("Storing plugin settings...");
            settings.ConfigVersion = PluginSettings.CurrentConfigVersion;
            var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(_configPath + @"config.json", json);
        }

        /// <summary>
        /// Read settings from config file
        /// </summary>
        /// <returns></returns>
        public PluginSettings GetSettings()
        {
            Logging.Log("Reading plugin settings...");
            var jsonString = File.ReadAllText(_configPath + @"config.json");
            return JsonConvert.DeserializeObject<PluginSettings>(jsonString);
        }
    }
}