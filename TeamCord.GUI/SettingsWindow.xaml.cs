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
        public SettingsModel SettingsModelBind { get; }

        public SettingsWindow(SettingsModel settings)
        {
            InitializeComponent();
            SettingsModelBind = settings;
            Logging.Log("Initializing SettingsWindow", LogLevel.LogLevel_DEBUG);

            if (SettingsModelBind.Token == null)
            {
                Logging.Log("Token missing. Opening login prompt...");
                var r = Forge.Forms.Show.Window().For(new LoginModel()).Result;
                if ((string)r.Action == "login")
                    Login(r.Model.Username, r.Model.Password);
            }
            else
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
        }

        private bool Login(string username, string password)
        {
            //TODO
            using (var auth = new Auth(username, password))
            {
                var token = auth.RequestToken();
                if (token == null)
                {
                    MessageBox.Show("Login failed. Possible reasons:\n- Wrong credentials" +
                        "\n- Login from new IP address (go to discord.com and login there once to complete a captcha and try again then)");
                    return false;
                }
                if (token.token != null)
                {
                    Store(token.token);
                    return true;
                }
                else if (token.mfa && token.ticket != null)
                {
                retry:
                    var r = Forge.Forms.Show.Window().For(new Forge.Forms.Prompt<string> { Title = "Insert two factor code" }).Result;
                    if (!r.Model.Confirmed)
                        return false;
                    token.Totp = r.Model.Value;
                    var mfaToken = auth.RequestMfaToken(token);
                    if (mfaToken != null)
                    {
                        Store(mfaToken);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Invalid 2fa code");
                        goto retry;
                    }
                }
                return false;
            }
            void Store(string token)
            {
                MessageBox.Show("Successfull logged in");
                var storage = new DataStorage<SettingsModel>();
                SettingsModelBind.Token = PluginUserCredential.StoreData(Encoding.Default.GetBytes(token));
                storage.Store(SettingsModelBind);
                buttonLogout.Visibility = Visibility.Visible;
            }
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