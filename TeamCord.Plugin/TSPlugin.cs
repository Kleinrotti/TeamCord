using System;
using System.Diagnostics;
using TeamCord.Core;
using TeamCord.GUI;
using TeamCord.Plugin.Natives;

namespace TeamCord.Plugin
{
    public sealed class TSPlugin : IPlugin
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
        public ConnectionHandler ConnectionHandler { get; private set; }
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

        private ushort ClientID
        {
            get
            {
                ulong srvHandler = Functions.getCurrentServerConnectionHandlerID();
                ushort id = 0;
                var err = Functions.getClientID(srvHandler, ref id);
                if (err != 0)
                    Logging.Log($"Requesting clientID failed: {err}");
                return id;
            }
        }

        public string PluginName { get; } = "TeamCord";
#if DEBUG
        public string PluginVersion { get; } = typeof(TSPlugin).Assembly.GetName().Version.ToString() + " [DEBUG build]";
#else
        public string PluginVersion { get; set; }= typeof(TSPlugin).Assembly.GetName().Version.ToString();
#endif
        public int ApiVersion { get; } = 23;
        public string Author { get; } = "Kleinrotti";
        public string Description { get; } = "Voice channel bridge between Teamspeak and Discord";
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
                Logging.Log("TeamCord " + typeof(TSPlugin).Assembly.GetName().Version.ToString());
                Logging.Log("Runtime CLR: " + Environment.Version);
                //if credentials are not stored disable login button and don't create ConnectionHandler
                if (Settings.Token == null)
                    Functions.setPluginMenuEnabled(PluginID, MenuItems.MenuItemConnect, false);
                else
                {
                    Functions.setPluginMenuEnabled(PluginID, MenuItems.MenuItemConnect, true);
                    ConnectionHandler = new ConnectionHandler(Settings.Token);
                    ConnectionHandler.ConnectionChanged += ConnectionHandler_ConnectionChanged;
                }
                Functions.setPluginMenuEnabled(PluginID, MenuItems.MenuItemDisconnect, false);
                Functions.setPluginMenuEnabled(PluginID, MenuItems.MenuItemJoin, false);
                Functions.setPluginMenuEnabled(PluginID, MenuItems.MenuItemLeave, false);
                Functions.setPluginMenuEnabled(PluginID, MenuItems.MenuItemConnectionInfo, false);
                Functions.setPluginMenuEnabled(PluginID, MenuItems.MenuItemLink, false);

                TrayIcon.Initialize();
                _trayIcon = new TrayIcon();
                TrayIcon.BalloonTimeout = 3;
                TrayIcon.Visible = true;
                TrayIcon.ShowNotifications = _settings.Notifications;
                TrayIcon.VolumeMenuItemClicked += TrayIcon_VolumeChangedClicked;
            }
            catch (Exception ex)
            {
                Instance.Functions.logMessage(ex.Message, LogLevel.LogLevel_ERROR, "TeamCord", 0);
                return 1;
            }
            finally
            {
                watch.Stop();
            }
            Logging.Log($"Teamcord initialized in {watch.ElapsedMilliseconds}ms");
            return 0;
        }

        #region Events

        private void ConnectionHandler_ConnectionChanged(object sender, ConnectionChangedEventArgs e)
        {
            //Enable/disable teamspeak menuitems
            try
            {
                switch (e.ConnectionType)
                {
                    case ConnectionType.Discord:
                        Functions.setPluginMenuEnabled(PluginID, MenuItems.MenuItemConnect, !e.Connected);
                        Functions.setPluginMenuEnabled(PluginID, MenuItems.MenuItemJoin, e.Connected);
                        Functions.setPluginMenuEnabled(PluginID, MenuItems.MenuItemDisconnect, e.Connected);
                        Functions.setPluginMenuEnabled(PluginID, MenuItems.MenuItemLink, e.Connected);
                        break;

                    case ConnectionType.Voice:
                        Functions.setPluginMenuEnabled(PluginID, MenuItems.MenuItemJoin, !e.Connected);
                        Functions.setPluginMenuEnabled(PluginID, MenuItems.MenuItemLeave, e.Connected);
                        Functions.setPluginMenuEnabled(PluginID, MenuItems.MenuItemConnectionInfo, e.Connected);
                        ApplyTs3MuteStateToDiscord();
                        UpdateClientDescription();
                        break;

                    case ConnectionType.Text:
                        break;

                    default:
                        break;
                }
            }
            //When Teamspeak will be closed/exited and a discord user is logged in, this could trigger an access violation before teamspeak is exited
            catch (AccessViolationException) { }
        }

        private void TrayIcon_VolumeChangedClicked(object sender, EventArgs e)
        {
            var _volumeControl = new VolumeControl(ConnectionHandler.UserVolumesInCurrentChannel);
            _volumeControl.VolumeChanged += Control_VolumeChanged;
            _volumeControl.Show();
        }

        private void Control_VolumeChanged(object sender, UserVolume e)
        {
            ConnectionHandler.CurrentVoiceChannelService.ChangeVolume(e);
        }

        #endregion Events

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
                {
                    Functions.flushChannelUpdates(serverConnectionHandlerID, ts3ChannelID, "");
                    Logging.Log("Linked discord channel successfully");
                }
            }
        }

        public void ShowConnectionInfo()
        {
            var connInfo = ConnectionHandler.ConnectionInfo;
            ConnectionInfoWindow connectionInfoWindow = new ConnectionInfoWindow(connInfo);
            connectionInfoWindow.ShowDialog();
        }

        public void ShowAboutWindow()
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }

        public void Shutdown()
        {
            if (ConnectionHandler != null)
            {
                ConnectionHandler.ConnectionChanged -= ConnectionHandler_ConnectionChanged;
                ConnectionHandler.Dispose();
            }
            if (_trayIcon != null)
            {
                TrayIcon.Visible = false;
                _trayIcon.Dispose();
                TrayIcon.VolumeMenuItemClicked -= TrayIcon_VolumeChangedClicked;
            }
            _settings = null;
        }

        public void Deaf(bool value)
        {
            ConnectionHandler.CurrentVoiceChannelService.Deaf = value;
        }

        public void Mute(bool value)
        {
            ConnectionHandler.CurrentVoiceChannelService.Mute = value;
        }

        public void ApplyTs3MuteStateToDiscord()
        {
            Logging.Log("Applying ts3 mute state...", LogLevel.LogLevel_DEBUG);
            int input = 0;
            int output = 0;
            ulong err = 0;
            ulong srvHandler = Functions.getCurrentServerConnectionHandlerID();
            err = Functions.getClientSelfVariableAsInt(srvHandler, ClientProperties.CLIENT_INPUT_MUTED, ref input);
            err += Functions.getClientSelfVariableAsInt(srvHandler, ClientProperties.CLIENT_OUTPUT_MUTED, ref output);
            if (err != (ulong)Ts3ErrorType.ERROR_ok)
            {
                Logging.Log($"Can't get ts3 mute state {err}");
            }
            ConnectionHandler.CurrentVoiceChannelService.Mute = input != 0;
            ConnectionHandler.CurrentVoiceChannelService.Deaf = output != 0;
        }

        private void UpdateClientDescription()
        {
            if (ConnectionHandler.OwnID == 0)
                return;
            ulong srvHandler = Functions.getCurrentServerConnectionHandlerID();

            var value = Helpers.DiscordIDToJsonString(ConnectionHandler.OwnID);
            string test = "";
            var err = Functions.requestClientEditDescription(srvHandler, ClientID, value, test);
            if (err != 0)
                Logging.Log($"Updating client description failed: {err}");
        }

        #region Logging

        private void Log(string message, LogLevel level)
        {
            Functions.logMessage(message, level, "TeamCord", 0);
        }

        private void Log(Exception exception, LogLevel level)
        {
            Functions.logMessage("Exception: " + exception.Message + "\nStacktrace: " + exception.StackTrace, level, "TeamCord", 0);
        }

        #endregion Logging
    }
}