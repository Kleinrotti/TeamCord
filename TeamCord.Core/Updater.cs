using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace TeamCord.Core
{
    /// <summary>
    /// Provides functionalities to check for updates
    /// </summary>
    public class Updater : UpdaterBase
    {
        /// <summary>
        /// Uri to the latest version available
        /// </summary>
        public virtual Uri LatestVersionUrl { get; protected set; }

        protected override string RequestUrl { get; set; } = "/repos/Kleinrotti/TeamCord/releases/latest";

        /// <summary>
        /// Indicates wether the newest release is a PreRelease version
        /// </summary>
        public virtual bool PreRelease { get; protected set; }

        public Updater(Version currentVersion) : base(currentVersion)
        {
            CurrentVersion = currentVersion;
        }

        /// <summary>
        /// Initiate update check
        /// </summary>
        public void CheckUpdate()
        {
            try
            {
                Logging.Log("Started update check", LogLevel.LogLevel_DEBUG);
                //github needs tls1.2
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(BaseUrl + RequestUrl);
                httpWebRequest.ContentType = "application/vnd.github.v3+json";
                httpWebRequest.Method = "GET";
                httpWebRequest.UserAgent = "dotnet/TeamCord";
                httpWebRequest.AllowAutoRedirect = false;

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var definition = new { tag_name = "", html_url = "", prerelease = false };
                    var versionString = JsonConvert.DeserializeAnonymousType(result, definition).tag_name;
                    LatestVersionUrl = new Uri(JsonConvert.DeserializeAnonymousType(result, definition).html_url);
                    PreRelease = JsonConvert.DeserializeAnonymousType(result, definition).prerelease;
                    LatestVersion = new Version(versionString);
                    Logging.Log("Update check completed", LogLevel.LogLevel_DEBUG);
                }
            }
            catch (Exception ex)
            {
                Logging.Log(ex);
            }
        }
    }
}