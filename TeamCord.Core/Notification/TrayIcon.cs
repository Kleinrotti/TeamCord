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
        protected static MenuItem _outputMenuItem;
        protected static MenuItem _micMenuItem;
        public static bool ShowNotifications { get; set; }
        public static int BalloonTimeout { get; set; } = 5;

        public static event EventHandler VolumeMenuItemClicked;

        public static event EventHandler<GenericEventArgs<bool>> OutputMenuItemClicked;

        public static event EventHandler<GenericEventArgs<bool>> MicMenuItemClicked;

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

        public static void Initialize()
        {
            Icon = new NotifyIcon();
            _volumeMenuItem = new MenuItem("Volume control", new EventHandler(OnVolumeMenuItemClick));
            _outputMenuItem = new MenuItem("Audio on", new EventHandler(OnOutputMenuItemClick));
            _volumeMenuItem.Checked = true;
            _outputMenuItem.Checked = true;
            _micMenuItem = new MenuItem("Mic on", new EventHandler(OnMicMenuItemClick));
            _volumeMenuItem.Enabled = false;
            Menu = new ContextMenu();
            Menu.MenuItems.Add(_volumeMenuItem);
            Menu.MenuItems.Add(_outputMenuItem);
            Menu.MenuItems.Add(_micMenuItem);
            Icon.Text = "TeamCord Status";
            Icon.Icon = Properties.Resource.logo;
            Icon.Visible = true;
            Icon.ContextMenu = Menu;
            Logging.Log("Tray icon loaded");
        }

        private static void OnVolumeMenuItemClick(object sender, EventArgs e)
        {
            VolumeMenuItemClicked?.Invoke(sender, e);
        }

        private static void OnOutputMenuItemClick(object sender, EventArgs e)
        {
            if (_outputMenuItem.Checked)
                _outputMenuItem.Checked = false;
            else
                _outputMenuItem.Checked = true;
            OutputMenuItemClicked?.Invoke(sender, new GenericEventArgs<bool>(_outputMenuItem.Checked));
        }

        private static void OnMicMenuItemClick(object sender, EventArgs e)
        {
            if (_micMenuItem.Checked)
                _micMenuItem.Checked = false;
            else
                _micMenuItem.Checked = true;
            MicMenuItemClicked?.Invoke(sender, new GenericEventArgs<bool>(_micMenuItem.Checked));
        }

        /// <summary>
        /// Display a notification
        /// </summary>
        /// <param name="notification"></param>
        public void ShowNotification(INotification notification)
        {
            if (ShowNotifications)
                Icon.ShowBalloonTip(BalloonTimeout, notification.Title, notification.Message, ToolTipIcon.Info);
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
        }
    }
}