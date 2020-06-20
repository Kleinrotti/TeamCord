using System.Collections.Generic;
using System.Windows.Forms;

namespace TeamCord.Core
{
    public class TrayIcon
    {
        protected static NotifyIcon Icon;
        protected static ContextMenu Menu;
        public static bool ShowNotifications { get; set; }
        public static int BalloonTimeout { get; set; } = 5;

        static TrayIcon()
        {
            Icon = new NotifyIcon();
            Menu = new ContextMenu();
            Icon.Text = "TeamCord Status";
            Icon.Icon = Properties.Resource.logo;
            Icon.Visible = true;
            Icon.ContextMenu = Menu;
        }

        public void ShowNotification(INotification notification)
        {
            Icon.ShowBalloonTip(BalloonTimeout, notification.Title, notification.Message, ToolTipIcon.Info);
        }

        public void UpdateContextMenu(IList<MenuItem> menuItems)
        {
            Menu.MenuItems.Clear();
            foreach (var v in menuItems)
            {
                Menu.MenuItems.Add(v);
            }
        }

        public void UpdateContextMenu(MenuItem menuItem)
        {
            Menu.MenuItems.Clear();

            Menu.MenuItems.Add(menuItem);
        }
    }
}