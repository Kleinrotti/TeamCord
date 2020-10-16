using System;
using System.IO;

namespace TeamCord.Core
{
    /// <summary>
    /// Read and write settings for TeamCord
    /// </summary>
    public class DataStorage<T> : IStorage<T> where T : IFileStorable<T>, new()
    {
        protected virtual string ConfigPath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TS3Client\plugins\TeamCord\";

        public DataStorage()
        {
            if (!File.Exists(ConfigPath + @"config.json"))
            {
                Directory.CreateDirectory(ConfigPath);
                Logging.Log("Config file does not exists, creating one...");
                Store(new T());
            }
        }

        /// <summary>
        /// Save settings to config file
        /// </summary>
        /// <param name="settings"></param>
        public virtual void Store(T settings)
        {
            Logging.Log("Storing plugin settings...");
            var json = settings.Serialize();
            File.WriteAllText(ConfigPath + @"config.json", json);
        }

        /// <summary>
        /// Read settings from config file
        /// </summary>
        /// <returns></returns>
        public virtual T Get()
        {
            Logging.Log("Reading plugin settings...");
            var jsonString = File.ReadAllText(ConfigPath + @"config.json");
            return new T().Deserialize(jsonString);
        }
    }
}