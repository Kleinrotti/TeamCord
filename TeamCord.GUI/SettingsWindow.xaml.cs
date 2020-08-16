using System.Text;
using System.Windows;
using TeamCord.Core;

namespace TeamCord.GUI
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public sealed partial class SettingsWindow : Window
    {
        private PluginSettings _settings;

        public SettingsWindow(PluginSettings settings)
        {
            InitializeComponent();
            _settings = settings;
            checkBox_Autojoin.IsChecked = _settings.AutomaticChannelJoin;
            checkBox_AutoLoginDiscord.IsChecked = _settings.DiscordAutoLogin;
            checkBox_ConnectionStatus.IsChecked = _settings.ShowConnectionStatus;
            checkBox_RawAudio.IsChecked = _settings.UseTeamspeakVoiceActivation;
            checkBox_DebugLogging.IsChecked = _settings.DebugLogging;
            checkBox_Notifications.IsChecked = _settings.Notifications;
            if (_settings.Email.Entropy != null && _settings.Email.CipherText != null)
                textBox_Email.Text = Encoding.Default.GetString(_settings.Email.GetStoredData());
            if (_settings.Password.Entropy != null && _settings.Password.CipherText != null)
                passwordBox_Password.Password = Encoding.Default.GetString(_settings.Password.GetStoredData());
        }

        private void button_Save_Click(object sender, RoutedEventArgs e)
        {
            var valid = Auth.ValidateCredentials(textBox_Email.Text, passwordBox_Password.Password);
            if (!valid)
                MessageBox.Show("Warning: Entered credentials are not valid!");
            var newSettings = new PluginSettings
            {
                AutomaticChannelJoin = checkBox_Autojoin.IsChecked ?? false,
                UseTeamspeakVoiceActivation = checkBox_RawAudio.IsChecked ?? false,
                DiscordAutoLogin = checkBox_AutoLoginDiscord.IsChecked ?? false,
                ShowConnectionStatus = checkBox_ConnectionStatus.IsChecked ?? false,
                DebugLogging = checkBox_DebugLogging.IsChecked ?? false,
                Notifications = checkBox_Notifications.IsChecked ?? false,
                Email = PluginUserCredential.StoreData(Encoding.Default.GetBytes(textBox_Email.Text)),
                Password = PluginUserCredential.StoreData(Encoding.Default.GetBytes(passwordBox_Password.Password))
            };
            var storage = new DataStorage();
            storage.StoreSettings(newSettings);
            _settings = newSettings;
            MessageBox.Show("Please reload TeamCord to apply all changed settings.");
            Close();
        }
    }
}