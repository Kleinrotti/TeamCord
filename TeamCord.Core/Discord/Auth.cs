using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace TeamCord.Core
{
    /// <summary>
    /// Authentication logic for discord
    /// </summary>
    public class Auth : IDisposable
    {
        private string _email;
        private string _password;

        public Auth(string email, string password)
        {
            _email = email;
            _password = password;
        }

        /// <summary>
        /// Request a discord login token
        /// </summary>
        /// <returns>Token</returns>
        public string RequestToken()
        {
            Logging.Log("Requesting login token");
            return getToken();
        }

        /// <summary>
        /// Check if the credentials are valid
        /// </summary>
        /// <returns>True if valid, false if not</returns>
        public bool ValidateCredentials()
        {
            Logging.Log("Validating credentials");
            var token = getToken();
            if (token != "")
            {
                token = null;
                return true;
            }
            else
                return false;
        }

        private string getToken()
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://discord.com/api/v8/auth/login");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(new
                    {
                        email = _email,
                        password = _password,
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

        public void Dispose()
        {
            _email = null;
            _password = null;
        }
    }
}