namespace TeamCord.Core
{
    /// <summary>
    /// Indicates what type of connection is established/destablished
    /// </summary>
    public enum ConnectionType
    {
        /// <summary>
        /// Connection to discord
        /// </summary>
        Discord,

        /// <summary>
        /// Connection to discord voice channel
        /// </summary>
        Voice,

        /// <summary>
        /// Connection to discord text channel
        /// </summary>
        Text,
    }
}