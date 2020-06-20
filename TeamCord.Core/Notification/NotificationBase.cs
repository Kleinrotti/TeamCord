namespace TeamCord.Core
{
    /// <summary>
    /// Base class for Notifications objects
    /// </summary>
    public abstract class NotificationBase : INotification
    {
        /// <summary>
        /// Message of the notification
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Title of the notification
        /// </summary>
        public string Title { get; }

        protected TrayIcon trayIcon { get; }

        /// <summary>
        /// Initializes a new notification object
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public NotificationBase(string title, string message)
        {
            Message = message;
            Title = title;
            trayIcon = new TrayIcon();
        }

        /// <summary>
        /// Display a notification on the tray icon
        /// </summary>
        /// <param name="trayIcon"></param>
        public virtual void Notify()
        {
            trayIcon.ShowNotification(this);
        }
    }
}