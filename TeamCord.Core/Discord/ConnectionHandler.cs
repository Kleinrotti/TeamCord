using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
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
        private AudioService _audioService;
        private string _token;
        private short[] _voiceBuffer;
        private IVoiceChannel _currentChannel;
        private Auth _auth;

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

        /// <summary>
        /// Return a list of tuples with each volume, userid and nickname
        /// </summary>
        public IList<UserVolume> UserVolumesInCurrentChannel
        {
            get
            {
                return _audioService.UserVolumes;
            }
        }

        public ConnectionHandler(Auth authentication)
        {
            _auth = authentication;
            _client = new DiscordSocketClient();

            _audioService = new AudioService();
            _audioService.VoiceConnected += _audioService_VoiceConnected;
            _audioService.VoiceDisconnected += _audioService_VoiceDisconnected;
            _client.Log += Client_Log;
            _client.Connected += _client_Connected;
            _client.Disconnected += _client_Disconnected;
            _client.LoggedOut += _client_LoggedOut;
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
            new ConnectionNotification("TeamCord", "").Notify(_currentChannel, ConnectionState.Disconnected);
            TrayIcon.VolumeMenuItemEnabled = false;
            _currentChannel = null;
        }

        private void _audioService_VoiceConnected(object sender, EventArgs e)
        {
            Logging.Log($"Client connected to voice");
            var status = new DiscordStatusNotification("TeamCord", "Status");
            status.UpdateStatus(ConnectionState.Connected);
            new ConnectionNotification("TeamCord", "").Notify(_currentChannel, ConnectionState.Connected);
            TrayIcon.VolumeMenuItemEnabled = true;
        }

        private Task _client_Disconnected(Exception arg)
        {
            Logging.Log($"Client disconnected");
            var status = new DiscordStatusNotification("TeamCord", "Status");
            status.UpdateStatus(_client.LoginState);
            return Task.CompletedTask;
        }

        private Task _client_Connected()
        {
            Logging.Log($"Client connected");
            var status = new DiscordStatusNotification("TeamCord", "Status");
            status.UpdateStatus(_client.LoginState);
            _audioService.OwnUserID = _client.CurrentUser.Id;
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
            if (_client.ConnectionState != ConnectionState.Connected || _client.ConnectionState == ConnectionState.Connecting)
            {
                try
                {
                    _token = _auth.RequestToken();
                    await _client.LoginAsync(0, _token);
                    await _client.StartAsync();
                }
                catch (Exception ex)
                {
                    Logging.Log(ex);
                }
            }
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
                await _audioService.JoinChannel(_currentChannel);
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
            await _audioService.LeaveChannel();
        }

        /// <summary>
        /// Disconnect and logout from discord
        /// </summary>
        public async void Disconnect()
        {
            if (_audioService != null)
                await _audioService.LeaveChannel();
            await _client.StopAsync();
            await _client.LogoutAsync();
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

            fixed (short* fo = _voiceBuffer)
            {
                byte* sample = (byte*)fo;
                for (int i = 0; i < _bufferBytes.Length; i++)
                {
                    _bufferBytes[i] = sample[i];
                }
            }

            Task.Run(() => { _audioService.SendVoiceData(_bufferBytes); });
        }

        private short[] ToStereo(short[] buf)
        {
            short[] buffer = new short[buf.Length * 2];
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
            _client.Dispose();
            _audioService.Dispose();
        }
    }
}