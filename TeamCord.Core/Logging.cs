using System;

namespace TeamCord.Core
{
    /// <summary>
    /// Class to handle logging in the TS3 client log
    /// </summary>
    public class Logging
    {
        private static Action<string, LogLevel> logCallback;
        private static Action<Exception, LogLevel> logCallbackException;
        public static bool DebugLogging { get; set; }

        /// <summary>
        /// Initialize this once with the callback function for logging in ts3
        /// </summary>
        /// <param name="ts3LogCallback"></param>
        public Logging(Action<string, LogLevel> ts3LogCallback, Action<Exception, LogLevel> ts3LogCallbackException)
        {
            logCallback = ts3LogCallback;
            logCallbackException = ts3LogCallbackException;
        }

        /// <summary>
        /// Log a message with specified log level
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logLevel"></param>
        public static void Log(string message, LogLevel logLevel = LogLevel.LogLevel_INFO)
        {
            if ((logLevel == LogLevel.LogLevel_DEBUG && DebugLogging) || logLevel != LogLevel.LogLevel_DEBUG)
                logCallback(message, logLevel);
        }

        /// <summary>
        /// Log a exception
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="logLevel"></param>
        public static void Log(Exception exception, LogLevel logLevel = LogLevel.LogLevel_ERROR)
        {
            logCallbackException(exception, logLevel);
        }
    }
}