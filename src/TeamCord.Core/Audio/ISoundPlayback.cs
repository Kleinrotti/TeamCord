namespace TeamCord.Core
{
    /// <summary>
    /// Interface for sound playback
    /// </summary>
    public interface ISoundPlayback
    {
        /// <summary>
        /// Add audio samples which should be played back
        /// </summary>
        /// <param name="buffer"></param>
        void AddSamples(byte[] buffer);

        /// <summary>
        /// Starts audio playback
        /// </summary>
        void StartPlayback();

        /// <summary>
        /// Stops audio playback
        /// </summary>
        void StopPlayback();
    }
}