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
            if (_settings.PluginUserCredentials.Entropy != null && _settings.PluginUserCredentials.CipherText != null)
                passwordBox_token.Password = Encoding.Default.GetString(_settings.PluginUserCredentials.GetStoredPassword());
        }

        private void SettingsWindow_Closed(object sender, EventArgs e)
        {
            Callback(_settings);
        }

        private void button_ShowToken_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        }

        private void button_ShowToken_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        }

        private void button_Save_Click(object sender, RoutedEventArgs e)
        {
            _settings.AutomaticChannelJoin = checkBox_Autojoin.IsChecked ?? false;
            _settings.UseTeamspeakVoiceActivation = checkBox_RawAudio.IsChecked ?? false;
            _settings.DiscordAutoLogin = checkBox_AutoLoginDiscord.IsChecked ?? false;
            _settings.ShowConnectionStatus = checkBox_ConnectionStatus.IsChecked ?? false;
            _settings.DebugLogging = checkBox_DebugLogging.IsChecked ?? false;
            _settings.PluginUserCredentials = PluginUserCredentials.StorePassword(Encoding.Default.GetBytes(passwordBox_token.Password));
            var storage = new DataStorage();
            storage.StoreSettings(_settings);
            Close();
        }
    }
}