namespace TeamCord.Core
{
    public class UserVolume
    {
        public ulong UserID { get; protected set; }
        public float Volume { get; protected set; }
        public string Username { get; set; }

        public UserVolume(ulong userID, float volume)
        {
            UserID = userID;
            Volume = volume;
        }

        public UserVolume(ulong userID, float volume, string userName)
        {
            UserID = userID;
            Volume = volume;
            Username = userName;
        }
    }
}