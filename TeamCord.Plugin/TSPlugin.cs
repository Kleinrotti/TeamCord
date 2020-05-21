using System;
using System.IO;
using System.Threading.Tasks;
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
        public string PluginVersion = "0.1";
        public int ApiVersion = 23;
        public string Author = "Kleinrotti";
        public string Description = "Bridge between Teamspeak and Discord";
        public string PluginID { get; set; }

        public int Init()
        {
            try
            {
                var dir = Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Teamcord\config");
                if (!File.Exists(_configPath))
                {
                    var storage = new DataStorage();
                    storage.StoreSettings(new PluginSettings());
                }
                ConnectionHandler = new ConnectionHandler(Settings.PluginUserCredentials.GetStoredPassword());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
            return 0;
        }

        private Task ConnectionHandler_Disconnected(Exception arg)
        {
            Functions.printMessageToCurrentTab("Disconnected from discord.");
            return Task.CompletedTask;
        }

        private Task ConnectionHandler_Connected()
        {
            Functions.printMessageToCurrentTab("Connected to discord.");
            return Task.CompletedTask;
        }

        public void Shutdown()
        {
            ConnectionHandler?.Dispose();
        }
    }
}