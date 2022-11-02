using Discord;

namespace TeamCord.Core
{
    public class DiscordStatusNotification : NotificationBase
    {
        /// <summary>
        /// State of the discord connection
        /// </summary>
        public ConnectionState ConnectionState { get; protected set; }

        /// <summary>
        /// State of the discord login
        /// </summary>
        public LoginState LoginState { get; protected set; }

        public DiscordStatusNotification(string title, string message) : base(title, message)
        {
            Title = title;
            Message = message;
        }

        public DiscordStatusNotification(string title, string message, ConnectionState connectionState) : base(title, message)
        {
            Title = title;
            Message = message;
            ConnectionState = connectionState;
        }

        public DiscordStatusNotification(string title, string message, LoginState loginState) : base(title, message)
        {
            Title = title;
            Message = message;
            LoginState = loginState;
        }

        /// <summary>
        /// Updates the icon and the icon text based on connection state
        /// </summary>
        /// <param name="connectionState"></param>
        public virtual void UpdateStatus(ConnectionState connectionState)
        {
            ConnectionState = connectionState;
            switch (connectionState)
            {
                case ConnectionState.Disconnected:
                    trayIcon.UpdateIcon(Properties.Resource.logo_connected);
                    trayIcon.IconText = "Connection established";
                    break;

                case ConnectionState.Connected:
                    trayIcon.UpdateIcon(Properties.Resource.logo_voice);
                    trayIcon.IconText = "Voice connected";
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Updates the icon and the icon text based on login state
        /// </summary>
        /// <param name="loginState"></param>
        public virtual void UpdateStatus(LoginState loginState)
        {
            LoginState = loginState;
            switch (loginState)
            {
                case LoginState.LoggedOut:
                    trayIcon.UpdateIcon(Properties.Resource.logo);
                    trayIcon.IconText = "Not connected";
                    break;

                case LoginState.LoggedIn:
                    trayIcon.UpdateIcon(Properties.Resource.logo_connected);
                    trayIcon.IconText = "Connection established";
                    break;

                default:
                    break;
            }
        }
    }
}