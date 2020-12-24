using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Navigation;
using TeamCord.Core;

namespace TeamCord.GUI
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        private Version _version = new Version(Assembly.GetExecutingAssembly().GetName().Version.ToString());

        public AboutWindow()
        {
            InitializeComponent();
            CreateControls();
        }

        private void CreateControls()
        {
            textBlockVersion.Text = "Version: " + _version.ToString();
            Hyperlink github = new Hyperlink();
            github.NavigateUri = new Uri("https://github.com/Kleinrotti/TeamCord");
            github.Inlines.Add("https://github.com/Kleinrotti/TeamCord");
            github.RequestNavigate += Github_RequestNavigate;
            textBlockGithub.Inlines.Add(github);
            Hyperlink bugReport = new Hyperlink();
            bugReport.NavigateUri = new Uri("https://github.com/Kleinrotti/TeamCord/issues");
            bugReport.Inlines.Add("Report a bug");
            bugReport.RequestNavigate += BugReport_RequestNavigate;
            textBlockBugReport.Inlines.Add(bugReport);
            Updater u = new Updater(_version);
            u.CheckUpdate();
            if (u.UpdateAvailable)
            {
                Hyperlink update = new Hyperlink();
                update.NavigateUri = u.LatestVersionUrl;
                update.Inlines.Add("Update available: Click to download");
                update.RequestNavigate += Github_RequestNavigate;
                textBlockUpdate.Inlines.Add(update);
            }
            else
            {
                textBlockUpdate.Text = "You have the latest version";
            }
        }

        private void BugReport_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(e.Uri.AbsoluteUri);
            e.Handled = true;
        }

        private void Github_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(e.Uri.AbsoluteUri);
            e.Handled = true;
        }
    }
}