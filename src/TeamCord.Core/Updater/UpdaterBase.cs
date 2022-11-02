using System;

namespace TeamCord.Core
{
    public abstract class UpdaterBase
    {
        protected Version CurrentVersion;
        protected Version LatestVersion;
        protected virtual string BaseUrl { get; set; } = "https://api.github.com";
        protected abstract string RequestUrl { get; set; }

        /// <summary>
        /// Returns true if a newer version is available
        /// </summary>
        public virtual bool UpdateAvailable
        {
            get
            {
                return CurrentVersion < LatestVersion;
            }
        }

        public virtual Version Version
        { get { return LatestVersion; } }

        internal UpdaterBase(Version currentVersion)
        {
            CurrentVersion = currentVersion;
        }
    }
}