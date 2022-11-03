using Discord;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Threading.Tasks;

namespace TeamCord.Core
{
    /// <summary>
    /// Provides logic to playback audio data of a discord user.
    /// </summary>
    internal class SoundService : ISoundPlayback, ISoundUser, IDisposable
    {
        protected BufferedWaveProvider _waveProvider;
        protected VolumeSampleProvider _volumeSampleProvider;
        protected readonly IVoiceChannel _voiceChannel;
        protected WaveOut _waveOut;
        private readonly int _randomizedUserId;

        public ulong UserID { get; }

        /// <summary>
        /// The name of the audio playback device which should be used.
        /// </summary>
        internal static string PlaybackDeviceName { get; set; }

        public float Volume
        {
            get
            {
                return _volumeSampleProvider.Volume;
            }
            set
            {
                _volumeSampleProvider.Volume = value;
                Logging.Log($"Volume of user {_randomizedUserId} changed to {value}", LogLevel.LogLevel_DEBUG);
            }
        }

        /// <summary>
        /// Master audio volume, 1.0 is full
        /// </summary>
        public float MasterVolume
        {
            get
            {
                return _waveOut.Volume;
            }
            set
            {
                _waveOut.Volume = value;
            }
        }

        public SoundService(ulong userId, IVoiceChannel voiceChannel)
        {
            UserID = userId;
            _voiceChannel = voiceChannel;
            //create a random userId for pseudo log messages
            _randomizedUserId = new Random().Next(1000, 100000);
            InitSpeakers();
            Logging.Log($"SoundService loaded for user {_randomizedUserId}");
        }

        public async Task<string> GetNickname()
        {
            var user = await _voiceChannel.GetUserAsync(UserID);
            return user.Nickname ?? user.Username;
        }

        public async Task<UserVolume> GetUserVolume()
        {
            return new UserVolume(UserID, _volumeSampleProvider.Volume, await GetNickname());
        }

        public void StartPlayback()
        {
            _waveOut.Play();
            Logging.Log($"Playback started of user {_randomizedUserId}");
        }

        public void StopPlayback()
        {
            _waveOut.Stop();
            Logging.Log($"Playback stopped of user {_randomizedUserId}");
        }

        /// <summary>
        /// Initialize the audio playback device.
        /// </summary>
        private void InitSpeakers()
        {
            _waveProvider = new BufferedWaveProvider(new WaveFormat(48000, 2))
            {
                DiscardOnBufferOverflow = true
            };
            _waveOut = new WaveOut();

            if (PlaybackDeviceName != null)
            {
                for (int i = 0; i < WaveOut.DeviceCount; i++)
                {
                    var device = WaveOut.GetCapabilities(i);
                    if (PlaybackDeviceName == device.ProductName)
                    {
                        _waveOut.DeviceNumber = i;
                        Logging.Log($"Using playback device: {i} {device.ProductName}", LogLevel.LogLevel_DEBUG);
                    }
                }
            }
            _waveOut.PlaybackStopped += _waveOut_PlaybackStopped;
            _volumeSampleProvider = new VolumeSampleProvider(_waveProvider.ToSampleProvider());
            _waveOut.Init(_volumeSampleProvider);
            Logging.Log($"Wave device {_waveOut.DeviceNumber} initialized. Samplerate: {_waveProvider.WaveFormat.SampleRate} " +
                $"Channels: {_waveProvider.WaveFormat.Channels}", LogLevel.LogLevel_DEBUG);
        }

        private void _waveOut_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (e.Exception == null)
                Logging.Log($"Sound playback automatically stoppped of user {_randomizedUserId}");
            else
                Logging.Log(e.Exception);
        }

        public void AddSamples(byte[] buffer)
        {
            _waveProvider.AddSamples(buffer, 0, buffer.Length);
        }

        public void Dispose()
        {
            _waveOut.Dispose();
            _waveProvider = null;
            Logging.Log($"Unloaded SoundService of user {_randomizedUserId}", LogLevel.LogLevel_DEBUG);
        }
    }
}