using Discord;

namespace TeamCord.Core
{
    public class ConnectionNotification : NotificationBase
    {
        /// <summary>
        /// Initialize a new instance of the ConnectionNotification class
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public ConnectionNotification(string message = "", string title = "TeamCord") : base(title, message)
        {
            Title = title;
            Message = message;
        }

        /// <summary>
        /// Displays a connection notification for the given channel
        /// </summary>
        /// <param name="channel"></param>
        public void Notify(IChannel channel, ConnectionState connectionState)
        {
            Message = connectionState.ToString() + ": Channel -> " + channel?.Name ?? "";
            base.Notify();
        }
    }
}