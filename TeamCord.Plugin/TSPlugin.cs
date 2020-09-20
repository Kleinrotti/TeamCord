using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
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

        public ushort ClientID
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

        public ulong CurrentChannel
        {
            get
            {
                ulong srvHandler = Functions.getCurrentServerConnectionHandlerID();
                ulong result = 0;
                var err = Functions.getChannelOfClient(srvHandler, ClientID, ref result);
                if (err != 0)
                    Logging.Log($"Getting current ts3 channel failed {err}", LogLevel.LogLevel_WARNING);
                return result;
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
                        DiscordAutoMuteAll();
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
            if (ConnectionHandler.OwnID == 0 || !Settings.EnableDiscordID)
                return;
            ulong srvHandler = Functions.getCurrentServerConnectionHandlerID();

            var value = Helpers.DiscordIDToJsonString(ConnectionHandler.OwnID);
            string test = "";
            var err = Functions.requestClientEditDescription(srvHandler, ClientID, value, test);
            if (err != 0)
                Logging.Log($"Updating client description failed: {err}");
        }

        /// <summary>
        /// Mute discord user in the same channel (to avoid doubled audio)
        /// </summary>
        /// <param name="serverConnectionHandler"></param>
        /// <param name="ts3UserId"></param>
        public void DiscordAutoMuteUser(ulong serverConnectionHandler, ushort ts3UserId)
        {
            if (ConnectionHandler.OwnID == 0 || !Settings.EnableDiscordID)
                return;
            string clientDescription = "";
            var err = Functions.getClientVariableAsString(serverConnectionHandler, ts3UserId, ClientProperties.CLIENT_DESCRIPTION, ref clientDescription);
            if (err != 0)
            {
                Logging.Log("DiscordAutoMuteUser: Could not get ts3 client description", LogLevel.LogLevel_WARNING);
                return;
            }
            var discordId = Helpers.ExtractClientID(clientDescription);
            if (discordId != 0)
                ConnectionHandler.CurrentVoiceChannelService.ChangeVolume(discordId, 0);
        }

        /// <summary>
        /// Mute all discord users which are in the same channel as the teamspeak users (to avoid doubled audio)
        /// </summary>
        private void DiscordAutoMuteAll()
        {
            if (ConnectionHandler.OwnID == 0 || !Settings.EnableDiscordID)
                return;
            var clients = GetDiscordClientIds();

            foreach (var v in clients)
            {
                ConnectionHandler.CurrentVoiceChannelService.ChangeVolume(v.Value, 0);
            }
        }

        private Dictionary<ushort, ulong> GetDiscordClientIds()
        {
            ulong srvHandler = Functions.getCurrentServerConnectionHandlerID();
            ulong currentChannel = 0;
            IntPtr ptr = new IntPtr();
            var err = Functions.getChannelOfClient(srvHandler, ClientID, ref currentChannel) != 0;

            err = Functions.getChannelClientList(srvHandler, currentChannel, ref ptr) != 0;

            //store ts3 client id and discord id
            Dictionary<ushort, ulong> clients = new Dictionary<ushort, ulong>();
            string clientDescription = "";
            //we dont know the array size so we looping up to 100 users in a ts3 channel and break later
            for (int i = 0; i < 100; i++)
            {
                var id = (ushort)Marshal.ReadInt16(ptr, i);
                //at value 0 array end is reached
                if (id == 0)
                    break;
                err = Functions.getClientVariableAsString(srvHandler, id, ClientProperties.CLIENT_DESCRIPTION, ref clientDescription) != 0;
                var discordid = Helpers.ExtractClientID(clientDescription);

                //add to dictionary if a discord id was found in the client description
                if (discordid != 0)
                    clients.Add(id, discordid);
            }

            Marshal.FreeHGlobal(ptr);
            if (err)
                Logging.Log("Reading clients descriptions failed", LogLevel.LogLevel_WARNING);
            return clients;
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