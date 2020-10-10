using NAudio.Wave;
using System;
using System.Threading;

namespace TeamCord.Core
{
    /// <summary>
    /// Playback mp3 audio files
    /// </summary>
    internal class Mp3SoundEffect : IEffectPlayback
    {
        protected WaveOut WaveOut;
        protected WaveStream Stream;
        protected string Url;

        /// <summary>
        /// Creates a new SoundEffect instance with a given url to mp3 file. Always use this class in a using block!
        /// </summary>
        /// <param name="url"></param>
        public Mp3SoundEffect(string url)
        {
            Url = url;
        }

        public virtual void LoadStream()
        {
            Stream = new BlockAlignReductionStream(WaveFormatConversionStream.CreatePcmStream(
                new Mp3FileReader(Url)));
        }

        public virtual void Play()
        {
            try
            {
                Stream.Position = 0;
                WaveOut = new WaveOut(WaveCallbackInfo.FunctionCallback());

                WaveOut.Init(Stream);
                WaveOut.Play();
                while (WaveOut.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                Logging.Log(ex);
            }
        }

        public virtual void Dispose()
        {
            WaveOut?.Dispose();
            Stream?.Dispose();
        }
    }
}