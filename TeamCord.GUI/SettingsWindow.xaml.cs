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
            checkBox_Discordid.IsChecked = _settings.EnableDiscordID;
            checkBox_AutoUpdateCheck.IsChecked = _settings.AutoUpdateCheck;
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
            newSettings.EnableDiscordID = checkBox_Discordid.IsChecked ?? false;
            newSettings.AutoUpdateCheck = checkBox_AutoUpdateCheck.IsChecked ?? false;
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
                if (token == null)
                {
                    MessageBox.Show("Login failed. Possible reasons:\n- Wrong credentials" +
                        "\n- Login from new IP address (go to discord.com and login there once to complete a captcha and try again then)");
                    return;
                }
                if (token.token != null)
                {
                    Store(token.token);
                }
                else if (token.mfa && token.ticket != null)
                {
                    TotpWindow totpWindow = new TotpWindow(totpCallback);
                    totpWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    totpWindow.ShowDialog();

                    void totpCallback(string totp)
                    {
                        token.Totp = totp;
                        var mfaToken = auth.RequestMfaToken(token);
                        if (mfaToken != null)
                            Store(mfaToken);
                        else
                            MessageBox.Show("Invalid 2fa code");
                    }
                }
            }
            void Store(string token)
            {
                MessageBox.Show("Successfull logged in");
                var storage = new DataStorage();
                _settings.Token = PluginUserCredential.StoreData(Encoding.Default.GetBytes(token));
                storage.StoreSettings(_settings);
                _changed = true;
                stackPanelLogin.Visibility = Visibility.Collapsed;
                buttonLogout.Visibility = Visibility.Visible;
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