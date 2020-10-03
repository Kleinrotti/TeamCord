using System;

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
        public string Message { get; protected set; }

        /// <summary>
        /// Title of the notification
        /// </summary>
        public string Title { get; protected set; }

        protected TrayIcon trayIcon { get; set; }

        /// <summary>
        /// Action which should be executed when the balloon tip is clicked
        /// </summary>
        public Action BalloonClick { get; protected set; }

        /// <summary>
        /// Initializes a new notification object
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public NotificationBase(string message, string title)
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