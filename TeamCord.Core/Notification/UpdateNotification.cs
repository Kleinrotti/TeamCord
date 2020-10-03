using System;

namespace TeamCord.Core
{
    public class UpdateNotification : NotificationBase
    {
        /// <summary>
        /// Create a update notification. Only pass the version as message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        public UpdateNotification(string message, string title = "Update") : base(message, title)
        {
            Message = $"There is a new version of TeamCord available: {message}";
            Title = title;
        }

        /// <summary>
        /// Display a notification on the tray icon with a given callback which should be called when balloon tip is clicked
        /// </summary>
        /// <param name="balloonClickAction"></param>
        public void Notify(Action balloonClickAction)
        {
            BalloonClick = balloonClickAction;
            base.Notify();
        }
    }
}