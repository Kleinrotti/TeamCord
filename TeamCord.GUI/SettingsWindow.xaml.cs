using System.Windows;
using TeamCord.Core;

namespace TeamCord.GUI
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public sealed partial class SettingsWindow : Window
    {
        public SettingsModel SettingsModelBind { get; }

        public SettingsWindow(SettingsModel settings)
        {
            InitializeComponent();
            SettingsModelBind = settings;
            Logging.Log("Initializing SettingsWindow", LogLevel.LogLevel_DEBUG);

            if (SettingsModelBind.Token != null)
            {
                buttonLogout.Visibility = Visibility.Visible;
            }
            ControlSettings.Model = SettingsModelBind;
            ControlSettings.OnAction += MyForm_OnAction;
            Closed += SettingsWindow_Closed;
        }

        private void MyForm_OnAction(object sender, Forge.Forms.ActionEventArgs e)
        {
            var storage = new DataStorage<SettingsModel>();
            storage.Store(SettingsModelBind);
            Close();
        }

        private void SettingsWindow_Closed(object sender, System.EventArgs e)
        {
            TCMessageBox.Show("To apply changed settings, a restart of Teamspeak is required!");
        }

        private void buttonLogout_Click(object sender, RoutedEventArgs e)
        {
            var storage = new DataStorage<SettingsModel>();
            SettingsModelBind.Token = null;
            storage.Store(SettingsModelBind);
            buttonLogout.Visibility = Visibility.Collapsed;
        }
    }
}