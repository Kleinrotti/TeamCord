namespace TeamCord.Core
{
    public interface ISoundPlayback
    {
        void AddSamples(byte[] buffer);

        void StartPlayback();

        void StopPlayback();
    }
}