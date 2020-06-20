using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using TeamCord.Core;
using TeamCord.DiscordLib;

namespace TeamCord.Plugin
{
    public class TSPlugin
    {
        #region singleton

        private readonly static Lazy<TSPlugin> _instance = new Lazy<TSPlugin>(() => new TSPlugin());

        private TSPlugin()
        {
        }

        public static TSPlugin Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        #endregion singleton

        public TS3Functions Functions { get; set; }
        public ConnectionHandler ConnectionHandler;
        private string _configPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Teamcord\config\config.json";
        private PluginSettings _settings;
        public List<TS3Device> Devices { get; set; }

        public PluginSettings Settings
        {
            get
            {
                if (_settings == null)
                {
                    var v = new DataStorage();
                    _settings = v.GetSettings();
                    return _settings;
                }
                else
                    return _settings;
            }
        }

        public string PluginName = "TeamCord";
        public string PluginVersion = typeof(TSPlugin).Assembly.GetName().Version.ToString();
        public int ApiVersion = 23;
        public string Author = "Kleinrotti";
        public string Description = "Bridge between Teamspeak and Discord";
        public string PluginID { get; set; }

        public int Init()
        {
            Stopwatch watch = new Stopwatch();
            try
            {
                watch.Start();
                var log = new Logging(Log);
                var dir = Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Teamcord\config");
                if (!File.Exists(_configPath))
                {
                    var storage = new DataStorage();
                    storage.StoreSettings(new PluginSettings());
                }
                ConnectionHandler = new ConnectionHandler(Settings.PluginUserCredentials.GetStoredPassword());
                Devices = new List<TS3Device>();
                TrayIcon.BalloonTimeout = 3;
            }
            catch (Exception ex)
            {
                Logging.Log(ex.Message, LogLevel.LogLevel_CRITICAL);
                return 1;
            }
            watch.Stop();
            Logging.Log($"Teamcord initialized in {watch.ElapsedMilliseconds}ms", LogLevel.LogLevel_INFO);
            return 0;
        }

        public void Shutdown()
        {
            if (ConnectionHandler != null)
                ConnectionHandler.Dispose();
        }

        private void Log(string message, LogLevel level)
        {
            Functions.logMessage(message, level, "TeamCord", 0);
        }
    }
}