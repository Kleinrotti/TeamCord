using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TeamCord.DiscordLib
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
        private IChannel _currentChannel;

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

        public ConnectionHandler(byte[] token)
        {
            _token = token;
            _client = new DiscordSocketClient();
            _audioService = new AudioService();
            _client.Log += Client_Log;
        }

        public IList<string> GetUsersInChannel(ulong channelId)
        {
            IList<string> list = new List<string>();
            try
            {

                var users = _client.GetChannel(channelId).Users;
                foreach (var v in users)
                {
                    list.Add(v.Username);
                }
            }
            catch(NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
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
                await _client.LoginAsync(0, Encoding.Default.GetString(_token));
                await _client.StartAsync();
            }
        }

        /// <summary>
        /// Connect to a discord voice channel
        /// </summary>
        /// <param name="channelID"></param>
        public async void JoinChannel(ulong channelID)
        {
            if (_client.ConnectionState != ConnectionState.Connecting || _client.ConnectionState != ConnectionState.Connected)
                Connect();
            _currentChannel = _client.GetChannel(channelID) as SocketVoiceChannel;
            await _audioService.JoinChannel((IVoiceChannel)_currentChannel);
        }

        public async void LeaveChannel()
        {
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

            _audioService.SendVoiceData(_bufferBytes);
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
        }
    }
}