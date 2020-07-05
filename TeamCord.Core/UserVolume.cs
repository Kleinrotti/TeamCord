namespace TeamCord.Core
{
    public class UserVolume
    {
        public ulong UserID { get; protected set; }
        public float Volume { get; set; }

        public UserVolume(ulong userID, float volume)
        {
            UserID = userID;
            Volume = volume;
        }
    }
}