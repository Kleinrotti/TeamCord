using System;
using System.Text;

namespace TeamCord.Core
{
    /// <summary>
    /// Class to handle logging in the TS3 client log
    /// </summary>
    public class Logging
    {
        private static Action<string, LogLevel> logCallback;
        private static Action<Exception, LogLevel> logCallbackException;

        /// <summary>
        /// Indicates wether debug logging is enabled or not
        /// </summary>
        public static bool DebugLogging { get; set; }

        /// <summary>
        /// Initialize this once with the callback function for logging in ts3
        /// </summary>
        /// <param name="ts3LogCallback"></param>
        public Logging(Action<string, LogLevel> ts3LogCallback, Action<Exception, LogLevel> ts3LogCallbackException)
        {
            logCallback = ts3LogCallback;
            logCallbackException = ts3LogCallbackException;
            Console.OutputEncoding = Encoding.Unicode;
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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[Teamcord] ");
            switch (logLevel)
            {
                case LogLevel.LogLevel_CRITICAL:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;

                case LogLevel.LogLevel_ERROR:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                case LogLevel.LogLevel_WARNING:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;

                case LogLevel.LogLevel_DEBUG:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;

                case LogLevel.LogLevel_INFO:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;

                case LogLevel.LogLevel_DEVEL:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;

                default:
                    break;
            }
            Console.Write(message + "\n");
        }

        /// <summary>
        /// Log an exception
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="logLevel"></param>
        public static void Log(Exception exception, LogLevel logLevel = LogLevel.LogLevel_ERROR)
        {
            //if debug logging stacktrace will be added
            if (DebugLogging)
                logCallbackException(exception, logLevel);
            else
                logCallback(exception.Message, logLevel);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[Teamcord] ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(exception.Message + "\n");
        }
    }
}