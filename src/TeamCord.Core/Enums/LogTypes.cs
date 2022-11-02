namespace TeamCord.Core
{
    public enum LogTypes
    {
        /// LogType_NONE -> 0x0000
        LogType_NONE = 0,

        /// LogType_FILE -> 0x0001
        LogType_FILE = 1,

        /// LogType_CONSOLE -> 0x0002
        LogType_CONSOLE = 2,

        /// LogType_USERLOGGING -> 0x0004
        LogType_USERLOGGING = 4,

        /// LogType_NO_NETLOGGING -> 0x0008
        LogType_NO_NETLOGGING = 8,

        /// LogType_DATABASE -> 0x0010
        LogType_DATABASE = 16,
    }
}