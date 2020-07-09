using System;
using System.Diagnostics;
using TeamCord.Core;
using TeamCord.GUI;

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
        private PluginSettings _settings;
        private TrayIcon _trayIcon;

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
                //logging with callback to ts3 client log
                var log = new Logging(Log, Log);
                Logging.DebugLogging = Settings.DebugLogging;
                ConnectionHandler = new ConnectionHandler(new Auth(Settings.Email, Settings.Password));
                _trayIcon = new TrayIcon();
                TrayIcon.BalloonTimeout = 3;
                TrayIcon.ShowNotifications = _settings.Notifications;
                TrayIcon.VolumeChangedClicked += TrayIcon_VolumeChangedClicked;
            }
            catch (Exception ex)
            {
                Logging.Log(ex, LogLevel.LogLevel_CRITICAL);
                return 1;
            }
            watch.Stop();
            Logging.Log($"Teamcord initialized in {watch.ElapsedMilliseconds}ms", LogLevel.LogLevel_INFO);
            return 0;
        }

        private void TrayIcon_VolumeChangedClicked(object sender, EventArgs e)
        {
            var _volumeControl = new VolumeControl(ConnectionHandler.UserVolumesInCurrentChannel);
            _volumeControl.VolumeChanged += Control_VolumeChanged;
            _volumeControl.Show();
        }

        private void Control_VolumeChanged(object sender, UserVolume e)
        {
            AudioService.ChangeVolume(e);
        }

        public void Shutdown()
        {
            if (ConnectionHandler != null)
                ConnectionHandler.Dispose();
            TrayIcon.Visible = false;
            _trayIcon.Dispose();
        }

        private void Log(string message, LogLevel level)
        {
            Functions.logMessage(message, level, "TeamCord", 0);
        }

        private void Log(Exception exception, LogLevel level)
        {
            Functions.logMessage("Exception: " + exception.Message + "\nStacktrace: " + exception.StackTrace, level, "TeamCord", 0);
        }
    }
}