using Discord;
using System.Windows.Forms;

namespace TeamCord.Core
{
    public class DiscordStatusNotification : NotificationBase
    {
        /// <summary>
        /// State of the discord connection
        /// </summary>
        public ConnectionState ConnectionState { get; }

        public DiscordStatusNotification(string title, string message) : base(title, message)
        {
        }

        public DiscordStatusNotification(string title, string message, ConnectionState connectionState) : base(title, message)
        {
            ConnectionState = connectionState;
        }

        public virtual void UpdateStatus()
        {
            var item = new MenuItem
            {
                Text = ConnectionState.ToString(),
                Enabled = false
            };
            trayIcon.UpdateContextMenu(item);
        }
    }
}