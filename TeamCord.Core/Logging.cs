using System;

namespace TeamCord.Core
{
    /// <summary>
    /// Class to handle logging in the TS3 client log
    /// </summary>
    public class Logging
    {
        private static Action<string, LogLevel> logCallback;

        /// <summary>
        /// Initialize this once with the callback function for logging in ts3
        /// </summary>
        /// <param name="ts3LogCallback"></param>
        public Logging(Action<string, LogLevel> ts3LogCallback)
        {
            logCallback = ts3LogCallback;
        }

        public static void Log(string message, LogLevel logLevel)
        {
            logCallback(message, logLevel);
        }
    }
}