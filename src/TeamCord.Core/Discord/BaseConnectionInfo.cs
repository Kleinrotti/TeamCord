using Discord.WebSocket;

namespace TeamCord.Core
{
    /// <summary>
    /// Base class for information of discord connections
    /// </summary>
    public abstract class BaseConnectionInfo
    {
        protected DiscordSocketClient Client;

        /// <summary>
        /// Latency to discord gateway
        /// </summary>
        [Unit("ms")]
        public virtual int Latency
        { get { return Client.Latency; } }

        /// <summary>
        /// Username of the logged in user
        /// </summary>
        public virtual string CurrentUsername
        { get { return Client.CurrentUser.Username; } }

        public BaseConnectionInfo(DiscordSocketClient client)
        {
            Client = client;
        }
    }
}