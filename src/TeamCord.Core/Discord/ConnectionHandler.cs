using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private Stopwatch _sw;
        private int _counter = 0;
        private double[] _times = new double[50];

        public event EventHandler<ConnectionChangedEventArgs> ConnectionChanged;

        public event EventHandler<GenericEventArgs<int>> AverageVoiceProcessTimeChanged;

        public bool Connected { get; private set; }

        public string PlaybackDevice
        {
            get
            {
                return SoundService.PlaybackDeviceName;
            }
            set
            {
                SoundService.PlaybackDeviceName = value;
            }
        }

        /// <summary>
        /// Returns the average time (us) to process a voice packet
        /// </summary>
        public int AverageVoiceProcessTime
        {
            get
            {
                return (int)_times.Average();
            }
        }

        public VoiceChannelService CurrentVoiceChannelService
        {
            get
            {
                return _voiceChannelService;
            }
        }

        /// <summary>
        /// Get all users in the current connected channel
        /// </summary>
        public IList<string> UsersInCurrentChannel
        {
            get
            {
                if (_currentChannel == null)
                    return new List<string>();
                var users = _client.GetChannel(_currentChannel.Id)?.Users;
                IList<string> list = new List<string>();
                if (users == null)
                    return list;
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

        public ulong OwnID
        {
            get
            {
                return _client?.CurrentUser?.Id ?? 0;
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

        public ConnectionHandler(PluginUserCredential token)
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
#if DEBUG
                LogLevel = LogSeverity.Debug,
#endif
                GatewayIntents = GatewayIntents.All
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
            //handle voice channel moving of user
            if (arg2.VoiceChannel != null && arg3.VoiceChannel != null && arg1.Id == OwnID)
            {
                //leave discord channel when another users moves me
                LeaveChannel();
                arg3.VoiceChannel.DisconnectAsync();
            }
            return Task.CompletedTask;
        }

        #region Connection events

        private Task _client_LoggedOut()
        {
            Logging.Log($"Client logged out");
            var status = new DiscordStatusNotification("TeamCord", "Status");
            status.UpdateStatus(LoginState.LoggedOut);
            return Task.CompletedTask;
        }

        private void _audioService_VoiceDisconnected(object sender, EventArgs e)
        {
            Logging.Log($"Client disconnected from voice");
            var status = new DiscordStatusNotification("TeamCord", "Status");
            status.UpdateStatus(ConnectionState.Disconnected);
            new ConnectionNotification().Notify(_currentChannel, ConnectionState.Disconnected);
            _currentChannel = null;
            ConnectionChanged?.Invoke(this, new ConnectionChangedEventArgs(ConnectionType.Voice, false));
            TrayIcon.VolumeMenuItemEnabled = false;
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
            status.UpdateStatus(LoginState.LoggedOut);
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

        /// <summary>
        /// Gets all users in the given channel
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns>A list of usernames</returns>
        public IList<string> GetUsersInChannel(ulong channelId)
        {
            IList<string> list = new List<string>();

            if (_client.ConnectionState != ConnectionState.Connected)
                return list;
            var users = _client.GetChannel(channelId)?.Users;
            if (users == null)
                return list;
            foreach (var v in users)
            {
                list.Add(v.Username);
            }

            return list;
        }

        private Task Client_Log(LogMessage arg)
        {
#if DEBUG
            Logging.Log("<Discord.net>" + arg.Message);
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
                    //wait for connection
                    while (_client.ConnectionState != ConnectionState.Connected)
                    {
                        await Task.Delay(10000);
                        if (_client.ConnectionState != ConnectionState.Connected)
                            throw new TimeoutException("Timeout reached while connection to Discord.");
                    }
                    Logging.Log("Connection established to discord");
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
                await _voiceChannelService.JoinChannel(_currentChannel);
                Logging.Log($"Channel bitrate: {_currentChannel.Bitrate}");
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
                await _client?.StopAsync();
                await _client?.LogoutAsync();
            }
            catch (Exception ex)
            {
                Logging.Log(ex, LogLevel.LogLevel_DEBUG);
            }
            _currentChannel = null;
        }

        /// <summary>
        /// Returns all servers that the client is in with all voice channels
        /// </summary>
        /// <returns></returns>
        public IList<TCServer> GetServerVoiceChannelList()
        {
            var guilds = _client.Guilds;
            var values = new List<TCServer>();
            if (guilds == null)
                return values;
            foreach (var v in guilds)
            {
                var guildChannels = v.Channels;
                var voiceServer = new TCServer(v.Id, v.Name);
                foreach (var x in guildChannels)
                {
                    //only add voice channels
                    if (x.GetType() == typeof(SocketVoiceChannel))
                        voiceServer.VoiceChannels.Add(new TCChannel(x.Id, x.Name));
                }
                values.Add(voiceServer);
            }
            return values;
        }

        /// <summary>
        /// Process and transmit audio data to discord
        /// </summary>
        /// <param name="samples"></param>
        /// <param name="sampleCount"></param>
        /// <param name="channels"></param>
        public unsafe void ProcessVoiceData(short* samples, int sampleCount, int channels)
        {
            if (_currentChannel == null)
                return;
            _sw = Stopwatch.StartNew();

            var audioSamples = new short[sampleCount * channels];
            //fill the short array with audio data
            for (int i = 0; i < audioSamples.Length; i++)
            {
                audioSamples[i] = *(samples + i);
            }

            //if sound data is PCM mono it needs to be converted to stereo for discord
            if (channels < 2)
                _voiceBuffer = ToStereo(audioSamples);
            else
                _voiceBuffer = audioSamples;

            if (_bufferBytes == null)
            {
                _bufferBytes = new byte[sizeof(short) * _voiceBuffer.Length];
            }
            //stereo short buffer needs to be converted to stereo byte buffer
            fixed (short* fo = _voiceBuffer)
            {
                var sample = (byte*)fo;
                for (int i = 0; i < _bufferBytes.Length; i++)
                {
                    _bufferBytes[i] = sample[i];
                }
            }
            _voiceChannelService.SendVoiceData(_bufferBytes);
            //measure performance
            _times[_counter] = _sw.Elapsed.TotalMilliseconds * 1000; //us
            if (_counter >= _times.Length - 1)
            {
                _counter = 0;
                OnProcessTimeChanged(this, new GenericEventArgs<int>(AverageVoiceProcessTime));
                Logging.Log($"Voice processing time: {AverageVoiceProcessTime}us ({channels} channel)", LogLevel.LogLevel_DEBUG);
            }
            else
            {
                _counter++;
            }
            _sw.Stop();
        }

        private void OnProcessTimeChanged(object sender, GenericEventArgs<int> e)
        {
            AverageVoiceProcessTimeChanged?.Invoke(sender, e);
        }

        private short[] ToStereo(short[] buf)
        {
            //new buffer needs to be twice as large because of stereo data
            var buffer = new short[buf.Length * 2];
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
            if (_client != null)
            {
                Disconnect();
                _client?.Dispose();
            }
            _voiceChannelService?.Dispose();
            _client = null;
            _bufferBytes = null;
            _voiceChannelService = null;
            _token = null;
            _voiceBuffer = null;
        }
    }
}