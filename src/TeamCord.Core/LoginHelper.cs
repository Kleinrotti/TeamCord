using System.Text;
using TeamCord.Core;

namespace TeamCord.GUI
{
    public static class LoginHelper
    {
        public static bool Login(ref SettingsModel settings)
        {
            var r = Forge.Forms.Show.Window().For(new LoginModel()).Result;
            if ((string)r.Action != "login")
                return false;

            using (var auth = new Auth(r.Model.Username, r.Model.Password))
            {
                string mfaToken = "";
                var token = auth.RequestToken();
                if (token == null)
                {
                    TCMessageBox.Show("Login failed. Possible reasons:\n-Wrong credentials" +
                        "\n-Login from new IP address (go to discord.com and login there once to complete a captcha and try again then)");
                    return false;
                }
                if (token.mfa && token.ticket != null && token.token == null)
                {
                retry:
                    var result = TCPrompt.Show("Insert two factor code");
                    if (!result.Confirmed)
                        return false;
                    token.Totp = result.Value;
                    mfaToken = auth.RequestMfaToken(token);
                    if (mfaToken == null)
                    {
                        TCMessageBox.Show("Invalid 2fa code");
                        goto retry;
                    }
                }
                else
                    mfaToken = token.token;
                TCMessageBox.Show("You are now logged in. Plugin reload required!");
                var storage = new DataStorage<SettingsModel>();
                settings.Token = PluginUserCredential.StoreData(Encoding.Default.GetBytes(mfaToken));
                storage.Store(settings);
                return true;
            }
        }
    }
}