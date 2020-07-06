﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TeamCord.Core
{
    public class TrayIcon : IDisposable
    {
        protected static NotifyIcon Icon;
        protected static ContextMenu Menu;
        public static bool ShowNotifications { get; set; }
        public static int BalloonTimeout { get; set; } = 5;

        public static event EventHandler VolumeChangedClicked;

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

        static TrayIcon()
        {
            Icon = new NotifyIcon();
            var volumeMenuItem = new MenuItem("Volume control", new EventHandler(OnVolumeMenuItemClick));
            Menu = new ContextMenu();
            Menu.MenuItems.Add(volumeMenuItem);
            Icon.Text = "TeamCord Status";
            Icon.Icon = Properties.Resource.logo;
            Icon.Visible = true;
            Icon.ContextMenu = Menu;
            Logging.Log("Tray icon loaded");
        }

        private static void OnVolumeMenuItemClick(object sender, EventArgs e)
        {
            VolumeChangedClicked?.Invoke(sender, e);
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