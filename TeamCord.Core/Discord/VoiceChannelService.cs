using Discord;
using Discord.Audio;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamCord.Core
{
    public class VoiceChannelService : IDisposable
    {
        private AudioStream _outStream;
        private IAudioClient _audioClient;
        private IVoiceChannel _voiceChannel;
        private static IList<SoundService> _soundServices;
        private DiscordSocketClient _socketClient;
        public ulong OwnUserID { get; set; }

        public event EventHandler VoiceConnected;

        public event EventHandler VoiceDisconnected;

        public VoiceChannelService(DiscordSocketClient socketClient, ulong ownUserID = 0)
        {
            OwnUserID = ownUserID;
            _soundServices = new List<SoundService>();
            _socketClient = socketClient;
        }

        /// <summary>
        /// Return a list of tuples with each volume, userid and nickname
        /// </summary>
        public IList<UserVolume> UserVolumes
        {
            get
            {
                var volumes = new List<UserVolume>();
                foreach (var v in _soundServices)
                {
                    volumes.Add(v.UserVolume);
                }
                return volumes;
            }
        }

        public static bool AudioOutput { get; set; }
        public static bool AudioInput { get; set; }

        [Command("join", RunMode = RunMode.Async)]
        public async Task JoinChannel(IVoiceChannel voiceChannel)
        {
            try
            {
                //await LeaveChannel();
                _voiceChannel = voiceChannel;
                _audioClient = await _voiceChannel.ConnectAsync();
                _audioClient.Disconnected += _audioClient_Disconnected;
                _audioClient.StreamCreated += _audioClient_StreamCreated;
                _audioClient.StreamDestroyed += _audioClient_StreamDestroyed;
                _outStream = _audioClient.CreatePCMStream(AudioApplication.Mixed, null, 100, 5);
                VoiceConnected?.Invoke(this, EventArgs.Empty);
                await ListenToUsersAsync();
            }
            catch (Exception ex) { Logging.Log(ex); }
        }

        private Task _audioClient_StreamDestroyed(ulong arg)
        {
            Logging.Log($"Stream destroyed {arg}");
            var v = _soundServices.SingleOrDefault(x => x.UserID == arg);
            if (v == null)
                return Task.CompletedTask;
            _soundServices.Remove(v);
            v.Dispose();
            return Task.CompletedTask;
        }

        private Task _audioClient_Disconnected(Exception arg)
        {
            VoiceDisconnected?.Invoke(this, EventArgs.Empty);
            return Task.CompletedTask;
        }

        private async Task ListenToUsersAsync()
        {
            var users = await _voiceChannel.GetUsersAsync().ToListAsync();
            foreach (var v in users[0])
            {
                //only play users audio data
                if (!v.IsBot)
                {
                    var socketUser = v as SocketGuildUser;
                    var userAudioStream = socketUser.AudioStream;
                    _ = Task.Run(() => { ListenUserAsync(userAudioStream, socketUser.Id); });
                }
            }
        }

        private Task _audioClient_StreamCreated(ulong userID, AudioInStream arg2)
        {
            //Triggers when user joined to the channel
            Logging.Log($"Stream created {userID}");
            return Task.Run(() => { ListenUserAsync(arg2, userID); });
        }

        [Command("leave", RunMode = RunMode.Async)]
        public async Task LeaveChannel()
        {
            if (_audioClient != null && _audioClient.ConnectionState == ConnectionState.Connected)
            {
                await _voiceChannel.DisconnectAsync();
                //await _audioClient.StopAsync();
            }
        }

        public async void SendVoiceData(byte[] buffer)
        {
            if (_audioClient != null && _outStream != null && _audioClient.ConnectionState == ConnectionState.Connected && AudioInput)
            {
                try
                {
                    await _outStream.WriteAsync(buffer, 0, buffer.Length);
                }
                catch (Exception ex)
                {
                    Logging.Log(ex);
                }
            }
        }

        /// <summary>
        /// Change the audio output volume of the specified stream
        /// </summary>
        /// <param name="streamID"></param>
        /// <param name="volume"></param>
        public static void ChangeVolume(ulong userID, float volume)
        {
            var sound = _soundServices.SingleOrDefault(x => x.UserID == userID);
            if (sound != null)
                sound.Volume = volume;
        }

        /// <summary>
        /// Change the audio output volume of the specified stream
        /// </summary>
        /// <param name="userVolume"></param>
        public static void ChangeVolume(UserVolume userVolume)
        {
            var sound = _soundServices.SingleOrDefault(x => x.UserID == userVolume.UserID);
            if (sound != null)
                sound.Volume = userVolume.Volume;
        }

        private async void ListenUserAsync(AudioInStream stream, ulong userID)
        {
            //do not playback own audio data
            if (userID == OwnUserID)
                return;

            var user = await _socketClient.Rest.GetGuildUserAsync(_voiceChannel.GuildId, userID);

            var soundsrv = new SoundService(userID, user.Nickname);
            _soundServices.Add(soundsrv);
            try
            {
                var buffer = new byte[3840];
                soundsrv.StartPlayback();
                while (await stream.ReadAsync(buffer, 0, buffer.Length) > 0)
                {
                    if (AudioOutput)
                        soundsrv.AddSamples(buffer);
                }
            }
            catch (Exception ex)
            {
                Logging.Log(ex);
            }
            finally
            {
                _soundServices.Remove(soundsrv);
                soundsrv.Dispose();
            }
        }

        public void Dispose()
        {
            foreach (var v in _soundServices)
            {
                v.Dispose();
            }
            _soundServices = null;
            if (_outStream != null)
                _outStream.Dispose();
            if (_audioClient != null)
                _audioClient.Dispose();
        }
    }
}