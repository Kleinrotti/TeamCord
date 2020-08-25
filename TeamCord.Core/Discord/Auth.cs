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
            Logging.Log("Requesting login token");
            return requestToken(Encoding.Default.GetString(_email.GetStoredData()),
                Encoding.Default.GetString(_password.GetStoredData()));
        }

        public static bool ValidateCredentials(string email, string password)
        {
            Logging.Log("Validating credentials");
            var token = requestToken(email, password);
            if (token != "")
                return true;
            else
                return false;
        }

        private static string requestToken(string email, string password)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://discord.com/api/v8/auth/login");
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
                    Logging.Log("Token successfully requested");
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