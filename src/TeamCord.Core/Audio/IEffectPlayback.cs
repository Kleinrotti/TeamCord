using System;

namespace TeamCord.Core
{
    /// <summary>
    /// Provides methods to load and play audio files
    /// </summary>
    internal interface IEffectPlayback : IDisposable
    {
        /// <summary>
        /// Initiates the stream for playback
        /// </summary>
        void LoadStream();

        /// <summary>
        /// Playback the audio data
        /// </summary>
        void Play();
    }
}