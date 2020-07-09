using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace TeamCord.Core
{
    public class Auth
    {
        private PluginUserCredential _email;
        private PluginUserCredential _password;

        public Auth(PluginUserCredential email, PluginUserCredential password)
        {
            _email = email;
            _password = password;
        }

        public string RequestToken()
        {
            return requestToken(Encoding.Default.GetString(_email.GetStoredData()),
                Encoding.Default.GetString(_password.GetStoredData()));
        }

        private string requestToken(string email, string password)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://discordapp.com/api/v6/auth/login");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(new
                    {
                        email,
                        password,
                    });

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var definition = new { token = "" };
                    var token = JsonConvert.DeserializeAnonymousType(result, definition).token;
                    return token;
                }
            }
            catch (Exception ex)
            {
                Logging.Log(ex);
                return string.Empty;
            }
        }
    }
}