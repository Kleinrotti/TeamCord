namespace TeamCord.Plugin.Natives
{
    public enum ReasonIdentifier
    {
        /// REASON_NONE -> 0
        REASON_NONE = 0,

        /// REASON_MOVED -> 1
        REASON_MOVED = 1,

        /// REASON_SUBSCRIPTION -> 2
        REASON_SUBSCRIPTION = 2,

        /// REASON_LOST_CONNECTION -> 3
        REASON_LOST_CONNECTION = 3,

        /// REASON_KICK_CHANNEL -> 4
        REASON_KICK_CHANNEL = 4,

        /// REASON_KICK_SERVER -> 5
        REASON_KICK_SERVER = 5,

        /// REASON_KICK_SERVER_BAN -> 6
        REASON_KICK_SERVER_BAN = 6,

        /// REASON_SERVERSTOP -> 7
        REASON_SERVERSTOP = 7,

        /// REASON_CLIENTDISCONNECT -> 8
        REASON_CLIENTDISCONNECT = 8,

        /// REASON_CHANNELUPDATE -> 9
        REASON_CHANNELUPDATE = 9,

        /// REASON_CHANNELEDIT -> 10
        REASON_CHANNELEDIT = 10,

        /// REASON_CLIENTDISCONNECT_SERVER_SHUTDOWN -> 11
        REASON_CLIENTDISCONNECT_SERVER_SHUTDOWN = 11,
    }
}