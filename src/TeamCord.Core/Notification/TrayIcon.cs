using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TeamCord.Core
{
    public class TrayIcon : IDisposable
    {
        protected static NotifyIcon Icon;
        protected static ContextMenu Menu;
        protected static MenuItem _volumeMenuItem;
        protected static Action OnBalloonClick;

        /// <summary>
        /// Enable or disable notifications
        /// </summary>
        public static bool ShowNotifications { get; set; }

        /// <summary>
        /// Timeout when the notification should disappear (default 3 sec.)
        /// </summary>
        public static int BalloonTimeout { get; set; } = 3;

        /// <summary>
        /// Handles clicks on the volume menu
        /// </summary>
        public static event EventHandler VolumeMenuItemClicked;

        /// <summary>
        /// Visibility of the Trayicon
        /// </summary>
        public static bool Visible
        {
            get
            {
                return Icon.Visible;
            }
            set
            {
                Icon.Visible = value;
            }
        }

        /// <summary>
        /// Get or sets a value wether the volume menu is enabled or not
        /// </summary>
        public static bool VolumeMenuItemEnabled
        {
            get
            {
                return _volumeMenuItem.Enabled;
            }
            set
            {
                _volumeMenuItem.Enabled = value;
            }
        }

        /// <summary>
        /// Trayicon mouse hover text
        /// </summary>
        public string IconText
        {
            get
            {
                return Icon.Text;
            }
            set
            {
                Icon.Text = "TeamCord: " + value;
            }
        }

        /// <summary>
        /// Initializes the tray icon
        /// </summary>
        public static void Initialize()
        {
            Icon = new NotifyIcon();
            _volumeMenuItem = new MenuItem("Volume control", new EventHandler(OnVolumeMenuItemClick));
            _volumeMenuItem.Enabled = false;
            Menu = new ContextMenu();
            Menu.MenuItems.Add(_volumeMenuItem);
            Icon.Text = "TeamCord Status";
            Icon.Icon = Properties.Resource.logo;
            Icon.Visible = true;
            Icon.ContextMenu = Menu;
            Icon.BalloonTipClicked += Icon_BalloonTipClicked;
            Logging.Log("Tray icon loaded");
        }

        private static void Icon_BalloonTipClicked(object sender, EventArgs e)
        {
            OnBalloonClick?.Invoke();
        }

        private static void OnVolumeMenuItemClick(object sender, EventArgs e)
        {
            VolumeMenuItemClicked?.Invoke(sender, e);
        }

        /// <summary>
        /// Display a notification
        /// </summary>
        /// <param name="notification"></param>
        public void ShowNotification(INotification notification)
        {
            if (ShowNotifications)
            {
                OnBalloonClick = notification.BalloonClick;
                Icon.ShowBalloonTip(BalloonTimeout, notification.Title, notification.Message, ToolTipIcon.Info);
            }
        }

        /// <summary>
        /// Change menuitems of the context menu
        /// </summary>
        /// <param name="menuItems"></param>
        public void UpdateContextMenu(IList<MenuItem> menuItems)
        {
            Menu.MenuItems.Clear();
            foreach (var v in menuItems)
            {
                Menu.MenuItems.Add(v);
            }
        }

        /// <summary>
        /// Change menuitems of the context menu
        /// </summary>
        /// <param name="menuItem"></param>
        public void UpdateContextMenu(MenuItem menuItem)
        {
            Menu.MenuItems.Clear();

            Menu.MenuItems.Add(menuItem);
        }

        /// <summary>
        /// Change the icon of the trayincon
        /// </summary>
        /// <param name="icon"></param>
        public void UpdateIcon(Icon icon)
        {
            Logging.Log("Updating icon of tray icon", LogLevel.LogLevel_DEBUG);
            Icon.Icon = icon;
        }

        public void Dispose()
        {
            Icon.Dispose();
            Menu.Dispose();
            Logging.Log("Tray icon unloaded");
        }
    }
}