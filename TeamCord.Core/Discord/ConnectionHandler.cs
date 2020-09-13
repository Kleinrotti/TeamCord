﻿using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamCord.Core.Notification;

namespace TeamCord.Core
{
    /// <summary>
    /// Discord connection handler class
    /// </summary>
    public class ConnectionHandler : IDisposable
    {
        private DiscordSocketClient _client;
        private byte[] _bufferBytes;
        private VoiceChannelService _voiceChannelService;
        private string _token = "";
        private short[] _voiceBuffer;
        private IVoiceChannel _currentChannel;

        public event EventHandler<ConnectionChangedEventArgs> ConnectionChanged;

        public bool Connected { get; private set; }

        public VoiceChannelService CurrentVoiceChannelService
        {
            get
            {
                return _voiceChannelService;
            }
        }

        /// <summary>
        /// Returns a list with the usernames w
        /// </summary>
        public IList<string> UsersInCurrentChannel
        {
            get
            {
                if (_currentChannel == null)
                    return new List<string>();
                var users = _client.GetChannel(_currentChannel.Id).Users;
                IList<string> list = new List<string>();
                foreach (var v in users)
                {
                    list.Add(v.Username);
                }
                return list;
            }
        }

        public string Username
        {
            get
            {
                if (_client.CurrentUser != null)
                    return _client.CurrentUser.Username;
                else
                    return "";
            }
        }

        public VoiceConnectionInfo ConnectionInfo
        {
            get
            {
                if (_currentChannel != null && _voiceChannelService != null)
                    return new VoiceConnectionInfo(_client, _currentChannel, _voiceChannelService.VoiceLatency);
                else
                    return null;
            }
        }

        /// <summary>
        /// Return a list of tuples with each volume, userid and nickname
        /// </summary>
        public IList<UserVolume> UserVolumesInCurrentChannel
        {
            get
            {
                return _voiceChannelService.UserVolumes;
            }
        }

        public ConnectionHandler(PluginUserCredential token)
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });
            _token = Encoding.Default.GetString(token.GetStoredData());
            _voiceChannelService = new VoiceChannelService(_client);
            _voiceChannelService.VoiceConnected += _audioService_VoiceConnected;
            _voiceChannelService.VoiceDisconnected += _audioService_VoiceDisconnected;
            _client.Log += Client_Log;
            _client.UserVoiceStateUpdated += _client_UserVoiceStateUpdated;
            _client.Ready += _client_Ready;
            _client.Connected += _client_Connected;
            _client.Disconnected += _client_Disconnected;
            _client.LoggedOut += _client_LoggedOut;
            Logging.Log("ConnectionHandler loaded");
        }

        private Task _client_UserVoiceStateUpdated(SocketUser arg1, SocketVoiceState arg2, SocketVoiceState arg3)
        {
            if (arg2.VoiceChannel.Id != arg3.VoiceChannel.Id)
            {
                //handle voice channel moving of user
            }
            return Task.CompletedTask;
        }

        #region Connection events

        private Task _client_LoggedOut()
        {
            Logging.Log($"Client logged out");
            var status = new DiscordStatusNotification("TeamCord", "Status");
            status.UpdateStatus(_client.LoginState);
            return Task.CompletedTask;
        }

        private void _audioService_VoiceDisconnected(object sender, EventArgs e)
        {
            Logging.Log($"Client disconnected from voice");
            var status = new DiscordStatusNotification("TeamCord", "Status");
            status.UpdateStatus(ConnectionState.Disconnected);
            new ConnectionNotification().Notify(_currentChannel, ConnectionState.Disconnected);
            ConnectionChanged?.Invoke(this, new ConnectionChangedEventArgs(ConnectionType.Voice, false));
            TrayIcon.VolumeMenuItemEnabled = false;
            _currentChannel = null;
        }

        private void _audioService_VoiceConnected(object sender, EventArgs e)
        {
            Logging.Log($"Client connected to voice");
            var status = new DiscordStatusNotification("TeamCord", "Status");
            status.UpdateStatus(ConnectionState.Connected);
            new ConnectionNotification().Notify(_currentChannel, ConnectionState.Connected);
            ConnectionChanged?.Invoke(this, new ConnectionChangedEventArgs(ConnectionType.Voice, true));
            TrayIcon.VolumeMenuItemEnabled = true;
        }

        private Task _client_Disconnected(Exception arg)
        {
            Logging.Log($"Client disconnected");
            Connected = false;
            var status = new DiscordStatusNotification("TeamCord", "Status");
            status.UpdateStatus(_client.LoginState);
            ConnectionChanged?.Invoke(this, new ConnectionChangedEventArgs(ConnectionType.Discord, false));
            return Task.CompletedTask;
        }

        private Task _client_Connected()
        {
            Logging.Log($"Client connected");
            Connected = true;
            var status = new DiscordStatusNotification("TeamCord", "Status");
            status.UpdateStatus(_client.LoginState);
            _voiceChannelService.OwnUserID = _client.CurrentUser.Id;
            return Task.CompletedTask;
        }

        private Task _client_Ready()
        {
            ConnectionChanged?.Invoke(this, new ConnectionChangedEventArgs(ConnectionType.Discord, true));
            return Task.CompletedTask;
        }

        #endregion Connection events

        public IList<string> GetUsersInChannel(ulong channelId)
        {
            IList<string> list = new List<string>();
            try
            {
                if (_client.ConnectionState != ConnectionState.Connected)
                    return list;
                var users = _client.GetChannel(channelId).Users;
                foreach (var v in users)
                {
                    list.Add(v.Username);
                }
            }
            catch (NullReferenceException ex)
            {
                Logging.Log(ex, LogLevel.LogLevel_DEBUG);
            }
            return list;
        }

        private Task Client_Log(LogMessage arg)
        {
#if DEBUG
            Console.WriteLine(arg.Message);
#endif
            return Task.CompletedTask;
        }

        /// <summary>
        /// Login and connect to discord
        /// </summary>
        public async Task Connect()
        {
            if (_client.ConnectionState != ConnectionState.Connected && _client.ConnectionState != ConnectionState.Connecting)
            {
                try
                {
                    await _client.LoginAsync(0, _token, false);
                    await _client.StartAsync();
                    Logging.Log("Waiting for established connection to discord...");
                    while (_client.ConnectionState != ConnectionState.Connected)
                    {
                        await Task.Delay(25);
                    }
                }
                catch (Exception ex)
                {
                    Logging.Log(ex);
                }
            }
        }

        /// <summary>
        /// Get the channel name
        /// </summary>
        /// <param name="channelID"></param>
        /// <returns></returns>
        public string GetChannelName(ulong channelID)
        {
            var channel = _client.GetChannel(channelID) as IGuildChannel;
            if (channel != null)
                return channel.Name;
            else
                return string.Empty;
        }

        /// <summary>
        /// Get the server name of a channel
        /// </summary>
        /// <param name="channelID"></param>
        /// <returns></returns>
        public string GetServerName(ulong channelID)
        {
            var server = _client.GetChannel(channelID) as IGuildChannel;
            if (server != null)
                return server.Guild.Name;
            else
                return string.Empty;
        }

        /// <summary>
        /// Connect to a discord voice channel
        /// </summary>
        /// <param name="channelID"></param>
        public async void JoinChannel(ulong channelID)
        {
            Logging.Log($"Joining discord channel {channelID}");
            await Connect();
            _currentChannel = _client.GetChannel(channelID) as SocketVoiceChannel;
            if (_currentChannel != null)
            {
                _voiceChannelService.JoinChannel(_currentChannel);
            }
            else
            {
                Logging.Log("Joining channel failed.", LogLevel.LogLevel_WARNING);
                new ConnectionNotification("Joining channel failed, check log for details").Notify();
            }
        }

        public async void LeaveChannel()
        {
            Logging.Log($"Leaving discord channel");
            await _voiceChannelService.LeaveChannel();
        }

        /// <summary>
        /// Disconnect and logout from discord
        /// </summary>
        public async void Disconnect()
        {
            if (_voiceChannelService != null)
                await _voiceChannelService.LeaveChannel();
            try
            {
                await _client.StopAsync();
                await _client.LogoutAsync();
            }
            catch (ObjectDisposedException ex)
            {
                Logging.Log(ex, LogLevel.LogLevel_DEBUG);
            }
            _currentChannel = null;
        }

        /// <summary>
        /// Returns all servers that the client is in with all voice channels
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, IDictionary<ulong, string>> GetServerVoiceChannelList()
        {
            var guilds = _client.Guilds;
            Dictionary<string, IDictionary<ulong, string>> values = new Dictionary<string, IDictionary<ulong, string>>();
            foreach (var v in guilds)
            {
                var guildChannels = v.Channels;
                var channels = new Dictionary<ulong, string>();
                foreach (var x in guildChannels)
                {
                    //only add voice channels
                    if (x.GetType() == typeof(SocketVoiceChannel))
                        channels.Add(x.Id, x.Name);
                }
                values.Add(v.Name, channels);
            }
            return values;
        }

        /// <summary>
        /// Transmit audio data to discord
        /// </summary>
        /// <param name="samples"></param>
        /// <param name="channels"></param>
        public unsafe void VoiceData(short[] samples, int channels)
        {
            //if sound data is PCM mono it needs to be converted to stereo for discord
            if (channels < 2)
                _voiceBuffer = ToStereo(samples);
            else
                _voiceBuffer = samples;

            if (_bufferBytes == null)
            {
                _bufferBytes = new byte[sizeof(short) * _voiceBuffer.Length];
            }

            //stereo short buffer needs to be converted to stereo byte buffer
            fixed (short* fo = _voiceBuffer)
            {
                byte* sample = (byte*)fo;
                for (int i = 0; i < _bufferBytes.Length; i++)
                {
                    _bufferBytes[i] = sample[i];
                }
            }

            Task.Run(() => { _voiceChannelService.SendVoiceData(_bufferBytes); });
        }

        private short[] ToStereo(short[] buf)
        {
            //new buffer needs to be twice as large because of stereo data
            short[] buffer = new short[buf.Length * 2];
            //start at the end and copy each short twice to the new stereo buffer
            for (int i = buf.Length - 1, j = buffer.Length - 1; i >= 0; --i)
            {
                buffer[j--] = buf[i];
                buffer[j--] = buf[i];
            }
            return buffer;
        }

        public void Dispose()
        {
            Disconnect();
            _voiceChannelService.Dispose();
            _client.Dispose();
            _client = null;
            _bufferBytes = null;
            _voiceChannelService = null;
            _token = null;
            _voiceBuffer = null;
        }
    }
}