using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;

namespace TeamCord.Core
{
    public class SoundService : ISoundPlayback, IDisposable
    {
        private BufferedWaveProvider _waveProvider;
        private VolumeSampleProvider _volumeSampleProvider;
        private WaveOut _waveOut;

        /// <summary>
        /// Volume of the user, 1.0 is full
        /// </summary>
        public float Volume
        {
            get
            {
                return _volumeSampleProvider.Volume;
            }
            set
            {
                _volumeSampleProvider.Volume = value;
                Logging.Log($"Volume of userID {UserID} changed to {value}", LogLevel.LogLevel_DEBUG);
            }
        }

        /// <summary>
        /// User audio volume object
        /// </summary>
        public UserVolume UserVolume
        {
            //TODO set volume
            get
            {
                return new UserVolume(UserID, _volumeSampleProvider.Volume);
            }
        }

        public ulong UserID { get; }
        public string Nickname { get; set; }

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

        public SoundService(ulong userID, string nickname)
        {
            UserID = userID;
            Nickname = nickname;
            InitSpeakers();
        }

        public void StartPlayback()
        {
            _waveOut.Play();
            Logging.Log($"Playback started of userID {UserID}");
        }

        public void StopPlayback()
        {
            _waveOut.Stop();
            Logging.Log($"Playback stopped of userID {UserID}");
        }

        private void InitSpeakers()
        {
            _waveProvider = new BufferedWaveProvider(new WaveFormat(48000, 2))
            {
                DiscardOnBufferOverflow = true
            };
            _waveOut = new WaveOut
            {
                DesiredLatency = 700,
                NumberOfBuffers = 3
            };
            _waveOut.PlaybackStopped += _waveOut_PlaybackStopped;
            _volumeSampleProvider = new VolumeSampleProvider(_waveProvider.ToSampleProvider());
            _waveOut.Init(_volumeSampleProvider);
            Logging.Log($"Wave device {_waveOut.DeviceNumber} initialized. Samplerate: {_waveProvider.WaveFormat.SampleRate} " +
                $"Channels: {_waveProvider.WaveFormat.Channels}", LogLevel.LogLevel_DEBUG);
        }

        private void _waveOut_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (e.Exception == null)
                Logging.Log($"Sound playback automatically stoppped of userID: {UserID}");
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
            Logging.Log($"Disposed SoundService of userID: {UserID}", LogLevel.LogLevel_DEBUG);
        }
    }
}