﻿using System.Text;
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
        private bool _changed;

        public SettingsWindow(PluginSettings settings)
        {
            Logging.Log("Initializing SettingsWindow", LogLevel.LogLevel_DEBUG);
            InitializeComponent();
            Closed += SettingsWindow_Closed;
            _settings = settings;
            checkBox_Autojoin.IsChecked = _settings.AutomaticChannelJoin;
            checkBox_AutoLoginDiscord.IsChecked = _settings.DiscordAutoLogin;
            checkBox_ConnectionStatus.IsChecked = _settings.ShowConnectionStatus;
            checkBox_RawAudio.IsChecked = _settings.UseTeamspeakVoiceActivation;
            checkBox_DebugLogging.IsChecked = _settings.DebugLogging;
            checkBox_Notifications.IsChecked = _settings.Notifications;
            if (_settings.Token != null)
            {
                stackPanelLogin.Visibility = Visibility.Collapsed;
                buttonLogout.Visibility = Visibility.Visible;
            }
        }

        private void SettingsWindow_Closed(object sender, System.EventArgs e)
        {
            if (_changed)
                MessageBox.Show("Please reload TeamCord to apply all changed settings.");
        }

        private void button_Save_Click(object sender, RoutedEventArgs e)
        {
            var newSettings = _settings;
            newSettings.AutomaticChannelJoin = checkBox_Autojoin.IsChecked ?? false;
            newSettings.UseTeamspeakVoiceActivation = checkBox_RawAudio.IsChecked ?? false;
            newSettings.DiscordAutoLogin = checkBox_AutoLoginDiscord.IsChecked ?? false;
            newSettings.ShowConnectionStatus = checkBox_ConnectionStatus.IsChecked ?? false;
            newSettings.DebugLogging = checkBox_DebugLogging.IsChecked ?? false;
            newSettings.Notifications = checkBox_Notifications.IsChecked ?? false;
            var storage = new DataStorage();
            storage.StoreSettings(newSettings);
            _changed = true;
            Close();
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            using (var auth = new Auth(textBox_Email.Text, passwordBox_Password.Password))
            {
                var token = auth.RequestToken();
                if (token != string.Empty)
                {
                    MessageBox.Show("Credentials are valid and login token stored encrypted.");
                    stackPanelLogin.Visibility = Visibility.Collapsed;
                    buttonLogout.Visibility = Visibility.Visible;
                    var storage = new DataStorage();
                    _settings.Token = PluginUserCredential.StoreData(Encoding.Default.GetBytes(token));
                    storage.StoreSettings(_settings);
                    _changed = true;
                }
                else
                {
                    MessageBox.Show("Entered credentials are not valid!");
                }
            }
        }

        private void buttonLogout_Click(object sender, RoutedEventArgs e)
        {
            _settings.Token = null;
            var storage = new DataStorage();
            storage.StoreSettings(_settings);
            _changed = true;
            stackPanelLogin.Visibility = Visibility.Visible;
            buttonLogout.Visibility = Visibility.Collapsed;
        }
    }
}