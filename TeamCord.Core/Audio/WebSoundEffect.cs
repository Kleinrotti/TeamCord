using NAudio.Wave;
using System;
using System.IO;
using System.Net;

namespace TeamCord.Core
{
    /// <summary>
    /// Playback mp3 audio from the web
    /// </summary>
    internal class WebMp3SoundEffect : Mp3SoundEffect
    {
        protected Stream WebStream;

        /// <summary>
        /// Creates a new WebSoundEffect instance with a given url to web mp3 file. Always use this class in a using block!
        /// </summary>
        /// <param name="url"></param>
        public WebMp3SoundEffect(string url) : base(url)
        {
            Url = url;
        }

        public override void LoadStream()
        {
            try
            {
                WebStream = new MemoryStream();

                using (Stream stream = WebRequest.Create(Url)
                    .GetResponse().GetResponseStream())
                {
                    byte[] buffer = new byte[32768];
                    int read;
                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        WebStream.Write(buffer, 0, read);
                    }
                }

                WebStream.Position = 0;
                Stream = new BlockAlignReductionStream(WaveFormatConversionStream.CreatePcmStream(
                    new Mp3FileReader(WebStream)));
            }
            catch (Exception ex)
            {
                Logging.Log(ex);
            }
        }

        public override void Dispose()
        {
            WaveOut?.Dispose();
            Stream?.Dispose();
            WebStream?.Dispose();
        }
    }
}