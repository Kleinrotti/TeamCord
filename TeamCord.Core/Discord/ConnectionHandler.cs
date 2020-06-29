using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
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
        private AudioService _audioService;
        private byte[] _token;
        private short[] _voiceBuffer;
        private IVoiceChannel _currentChannel;

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
        public IList<Tuple<float, ulong, string>> UserVolumesInCurrentChannel
        {
            get
            {
                return _audioService.UserVolumes;
            }
        }

        public ConnectionHandler(byte[] token)
        {
            _token = token;
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
            Logging.Log($"Client disconnected to voice");
            var status = new DiscordStatusNotification("TeamCord", "Status");
            status.UpdateStatus(ConnectionState.Disconnected);
        }

        private void _audioService_VoiceConnected(object sender, EventArgs e)
        {
            Logging.Log($"Client connected to voice");
            var status = new DiscordStatusNotification("TeamCord", "Status");
            status.UpdateStatus(ConnectionState.Connected);
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
                Logging.Log(ex);
            }
            return list;
        }

        private Task Client_Log(LogMessage arg)
        {
            Console.WriteLine(arg.Message);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Login and connect to discord
        /// </summary>
        public async void Connect()
        {
            if (_client.ConnectionState != ConnectionState.Connected || _client.ConnectionState == ConnectionState.Connecting)
            {
                try
                {
                    await _client.LoginAsync(0, Encoding.Default.GetString(_token));
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
            if (_client.ConnectionState != ConnectionState.Connecting || _client.ConnectionState != ConnectionState.Connected)
                Connect();
            _currentChannel = _client.GetChannel(channelID) as SocketVoiceChannel;
            await _audioService.JoinChannel((IVoiceChannel)_currentChannel);
        }

        public async void LeaveChannel()
        {
            Logging.Log($"Leaving discord channel {_currentChannel.Id}");
            await _audioService.LeaveChannel();
            _currentChannel = null;
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