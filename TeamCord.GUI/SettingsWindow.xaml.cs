using System;
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
        public Action<PluginSettings> Callback { get; set; }
        private PluginSettings _settings;

        public SettingsWindow(PluginSettings settings)
        {
            InitializeComponent();
            this.Closed += SettingsWindow_Closed;
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

        private void SettingsWindow_Closed(object sender, EventArgs e)
        {
            Callback(_settings);
        }

        private void button_Save_Click(object sender, RoutedEventArgs e)
        {
            _settings.AutomaticChannelJoin = checkBox_Autojoin.IsChecked ?? false;
            _settings.UseTeamspeakVoiceActivation = checkBox_RawAudio.IsChecked ?? false;
            _settings.DiscordAutoLogin = checkBox_AutoLoginDiscord.IsChecked ?? false;
            _settings.ShowConnectionStatus = checkBox_ConnectionStatus.IsChecked ?? false;
            _settings.DebugLogging = checkBox_DebugLogging.IsChecked ?? false;
            _settings.Notifications = checkBox_Notifications.IsChecked ?? false;
            _settings.Email = PluginUserCredential.StoreData(Encoding.Default.GetBytes(textBox_Email.Text));
            _settings.Password = PluginUserCredential.StoreData(Encoding.Default.GetBytes(passwordBox_Password.Password));
            var storage = new DataStorage();
            storage.StoreSettings(_settings);
            MessageBox.Show("Please restart Teamspeak to apply all changed settings.");
            Close();
        }
    }
}