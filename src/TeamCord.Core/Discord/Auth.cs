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
        /// <returns>AuthResult object which could store the token or indicates if MFA is enabled</returns>
        public AuthResult RequestToken()
        {
            Logging.Log("Requesting login token");
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
                        email = _email,
                        password = _password,
                    });

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var definition = new { token = "", mfa = false, ticket = "" };
                    var r = JsonConvert.DeserializeObject<AuthResult>(result);
                    Logging.Log("Received json response", LogLevel.LogLevel_DEBUG);
                    return r;
                }
            }
            catch (Exception ex)
            {
                Logging.Log(ex);
                return null;
            }
        }

        /// <summary>
        /// Request a discord mfa login token. Be sure that you set Totp property before.
        /// </summary>
        /// <param name="OAuthCode"></param>
        /// <returns></returns>
        public string RequestMfaToken(AuthResult authResult)
        {
            Logging.Log("Requesting mfa login token");
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://discord.com/api/v8/auth/mfa/totp");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(new
                    {
                        code = authResult.Totp,
                        authResult.ticket,
                    });

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var definition = new { token = "" };
                    var token = JsonConvert.DeserializeAnonymousType(result, definition).token;
                    if (token != null)
                        Logging.Log("MFA token successfully requested");
                    else
                        Logging.Log("Invalid 2fa code");
                    return token;
                }
            }
            catch (Exception ex)
            {
                Logging.Log(ex);
                return null;
            }
        }

        public void Dispose()
        {
            _email = null;
            _password = null;
        }
    }
}