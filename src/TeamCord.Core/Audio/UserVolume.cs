namespace TeamCord.Core
{
    /// <summary>
    /// Represents a user based object with a given output volume
    /// </summary>
    public class UserVolume
    {
        /// <summary>
        /// Unique identifier of the user
        /// </summary>
        public ulong UserID { get; protected set; }

        /// <summary>
        /// Current volume of the user
        /// </summary>
        public float Volume { get; protected set; }

        /// <summary>
        /// Users username
        /// </summary>
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