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
        private const string _audioJoin = "user_join.mp3";
        private const string _audioLeave = "user_leave.mp3";
        private const string _audioDisconnected = "voice_disconnected.mp3";

        private AudioOutStream _outStream;
        private IAudioClient _audioClient;
        private IVoiceChannel _voiceChannel;
        private static IList<SoundService> _soundServices;
        private DiscordSocketClient _socketClient;
        internal ulong OwnUserID { get; set; }

        internal event EventHandler VoiceConnected;

        internal event EventHandler VoiceDisconnected;

        /// <summary>
        /// When a user joins or leaves a channel.
        /// </summary>
        public static event EventHandler ChannelStateChanged;

        internal int VoiceLatency
        { get { return _audioClient.UdpLatency; } }

        internal VoiceChannelService(DiscordSocketClient socketClient, ulong ownUserID = 0)
        {
            OwnUserID = ownUserID;
            _soundServices = new List<SoundService>();
            _socketClient = socketClient;
            Logging.Log("VoiceChannelService loaded");
        }

        /// <summary>
        /// Returns a list of UserVolume objects
        /// </summary>
        public async Task<IList<UserVolume>> GetUserVolumes()
        {
            var volumes = new List<UserVolume>();
            foreach (var v in _soundServices)
            {
                volumes.Add(await v.GetUserVolume());
            }
            return volumes;
        }

        public bool Deaf { get; set; }
        public bool Mute { get; set; }

        //disabled due to disconnection bug
        //public bool Deaf
        //{
        //get
        //{
        //var user = _voiceChannel?.Guild.GetUserAsync(OwnUserID).Result;
        //if (user == null) return false;

        //return user.IsSelfDeafened;
        //}
        //set
        //{
        //_voiceChannel?.ModifyAsync(x =>
        //{
        //x.SelfDeaf = value;
        //});
        //}
        //}

        //public bool Mute
        //{
        //get
        //{
        //var user = _voiceChannel?.Guild.GetUserAsync(OwnUserID).Result;
        //if (user == null) return false;

        //return user.IsSelfMuted;

        //}
        //set
        //{
        //_voiceChannel?.ModifyAsync(x =>
        //{
        //x.SelfMute = value;
        //});
        //}
        //}

        [Command("join", RunMode = RunMode.Async)]
        internal async Task JoinChannel(IVoiceChannel voiceChannel)
        {
            try
            {
                _voiceChannel = voiceChannel;
                _audioClient = await _voiceChannel.ConnectAsync();
                _audioClient.Disconnected += _audioClient_Disconnected;
                _audioClient.StreamCreated += _audioClient_StreamCreated;
                _audioClient.StreamDestroyed += _audioClient_StreamDestroyed;
                _outStream = _audioClient.CreatePCMStream(AudioApplication.Mixed);
                VoiceConnected?.Invoke(this, EventArgs.Empty);
                ChannelStateChanged?.Invoke(this, EventArgs.Empty);
                using (IEffectPlayback joinSound = new Mp3SoundEffect(_audioJoin))
                {
                    joinSound.LoadStream();
                    joinSound.Play();
                }
                await ListenToUsersAsync();
            }
            catch (Exception ex) { Logging.Log(ex); }
        }

        private Task _audioClient_StreamCreated(ulong userID, AudioInStream arg2)
        {
            //Triggers when user joined to the channel
            Logging.Log($"Stream created {userID}", LogLevel.LogLevel_DEBUG);
            ChannelStateChanged?.Invoke(this, new EventArgs());
            using (IEffectPlayback joinSound = new Mp3SoundEffect(_audioJoin))
            {
                joinSound.LoadStream();
                joinSound.Play();
            }
            return Task.Run(() => { ListenUserAsync(arg2, userID); });
        }

        private Task _audioClient_StreamDestroyed(ulong arg)
        {
            Logging.Log($"Stream destroyed {arg}", LogLevel.LogLevel_DEBUG);
            ChannelStateChanged?.Invoke(this, new EventArgs());
            var v = _soundServices.SingleOrDefault(x => x.UserID == arg);
            if (v == null)
                return Task.CompletedTask;
            _soundServices.Remove(v);
            v.Dispose();
            //play sound when a users leaves the channel
            using (IEffectPlayback leaveSound = new Mp3SoundEffect(_audioLeave))
            {
                leaveSound.LoadStream();
                leaveSound.Play();
            }
            return Task.CompletedTask;
        }

        private Task _audioClient_Disconnected(Exception arg)
        {
            VoiceDisconnected?.Invoke(this, EventArgs.Empty);
            ChannelStateChanged?.Invoke(this, EventArgs.Empty);
            using (IEffectPlayback leaveSound = new Mp3SoundEffect(_audioDisconnected))
            {
                leaveSound.LoadStream();
                leaveSound.Play();
            }
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

        [Command("leave", RunMode = RunMode.Async)]
        internal async Task LeaveChannel()
        {
            if (_audioClient != null && _audioClient.ConnectionState == ConnectionState.Connected)
            {
                await _voiceChannel.DisconnectAsync();
            }
        }

        [Command(RunMode = RunMode.Async)]
        internal async void SendVoiceData(byte[] buffer)
        {
            if (_audioClient != null && _outStream != null && !Mute)
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
        public void ChangeVolume(ulong userID, float volume)
        {
            var sound = _soundServices.SingleOrDefault(x => x.UserID == userID);
            if (sound != null)
                sound.Volume = volume;
        }

        /// <summary>
        /// Change the audio output volume of the specified stream
        /// </summary>
        /// <param name="userVolume"></param>
        public void ChangeVolume(UserVolume userVolume)
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
            var user = await _voiceChannel.GetUserAsync(userID);

            //if user has no nickname set use username
            var soundsrv = new SoundService(userID, _voiceChannel);
            _soundServices.Add(soundsrv);
            try
            {
                await Task.Run(async () =>
                {
                    var buffer = new byte[3840];
                    soundsrv.StartPlayback();
                    while (await stream.ReadAsync(buffer, 0, buffer.Length) > 0)
                    {
                        if (!Deaf)
                            soundsrv.AddSamples(buffer);
                    }
                });
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
            Logging.Log("VoiceChannelService unloaded");
        }
    }
}