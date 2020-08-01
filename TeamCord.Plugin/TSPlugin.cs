using System;
using System.Diagnostics;
using TeamCord.Core;
using TeamCord.GUI;
using TeamCord.Plugin.Natives;

namespace TeamCord.Plugin
{
    public sealed class TSPlugin
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
#if DEBUG
        public string PluginVersion = typeof(TSPlugin).Assembly.GetName().Version.ToString() + " [DEBUG build]";
#else
        public string PluginVersion = typeof(TSPlugin).Assembly.GetName().Version.ToString();
#endif
        public int ApiVersion = 23;
        public string Author = "Kleinrotti";
        public string Description = "Voice channel bridge between Teamspeak and Discord";
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
                ConnectionHandler.ConnectionChanged += ConnectionHandler_ConnectionChanged;
                Functions.setPluginMenuEnabled(PluginID, 4, false);
                Functions.setPluginMenuEnabled(PluginID, 6, false);
                TrayIcon.Initialize();
                _trayIcon = new TrayIcon();
                TrayIcon.BalloonTimeout = 3;
                TrayIcon.Visible = true;
                TrayIcon.ShowNotifications = _settings.Notifications;
                TrayIcon.VolumeMenuItemClicked += TrayIcon_VolumeChangedClicked;
                TrayIcon.MicMenuItemClicked += TrayIcon_MicMenuItemClicked;
                TrayIcon.OutputMenuItemClicked += TrayIcon_OutputMenuItemClicked;
            }
            catch (Exception ex)
            {
                Logging.Log(ex);
                return 1;
            }
            watch.Stop();
            Logging.Log($"Teamcord initialized in {watch.ElapsedMilliseconds}ms", LogLevel.LogLevel_INFO);
            return 0;
        }

        private void ConnectionHandler_ConnectionChanged(object sender, GenericEventArgs<bool> e)
        {
            //Enable/disable teamspeak menuitems
            Functions.setPluginMenuEnabled(PluginID, 4, e.Data);
            Functions.setPluginMenuEnabled(PluginID, 5, !e.Data);
            Functions.setPluginMenuEnabled(PluginID, 6, e.Data);
        }

        private void TrayIcon_OutputMenuItemClicked(object sender, GenericEventArgs<bool> e)
        {
            VoiceChannelService.AudioOutput = e.Data;
        }

        private void TrayIcon_MicMenuItemClicked(object sender, GenericEventArgs<bool> e)
        {
            VoiceChannelService.AudioInput = e.Data;
        }

        private void TrayIcon_VolumeChangedClicked(object sender, EventArgs e)
        {
            var _volumeControl = new VolumeControl(ConnectionHandler.UserVolumesInCurrentChannel);
            _volumeControl.VolumeChanged += Control_VolumeChanged;
            _volumeControl.Show();
        }

        private void Control_VolumeChanged(object sender, UserVolume e)
        {
            VoiceChannelService.ChangeVolume(e);
        }

        public void LinkDiscordChannel(ulong serverConnectionHandlerID, ulong ts3ChannelID)
        {
            var list = ConnectionHandler.GetServerVoiceChannelList();
            ChannelConnector c = new ChannelConnector(list, callback);
            c.ShowDialog();
            void callback(ulong channelID)
            {
                var description = Helpers.ChannelIDToJsonString(channelID);
                var err = Functions.setChannelVariableAsString(serverConnectionHandlerID, ts3ChannelID, ChannelProperties.CHANNEL_DESCRIPTION, description);
                if (err != (uint)Ts3ErrorType.ERROR_ok)
                    Logging.Log($"Failed to set ts3channeldescription. Code: {err}", LogLevel.LogLevel_ERROR);
                else
                    Functions.flushChannelUpdates(serverConnectionHandlerID, ts3ChannelID, "");
            }
        }

        public void Shutdown()
        {
            if (ConnectionHandler != null)
                ConnectionHandler.Dispose();
            TrayIcon.Visible = false;
            _trayIcon.Dispose();
            _settings = null;
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