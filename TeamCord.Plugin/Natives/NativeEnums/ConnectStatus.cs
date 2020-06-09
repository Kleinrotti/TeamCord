namespace TeamCord.Plugin.Natives
{
    public enum ConnectStatus
    {
        /// STATUS_DISCONNECTED -> 0
        STATUS_DISCONNECTED = 0,

        STATUS_CONNECTING,

        STATUS_CONNECTED,

        STATUS_CONNECTION_ESTABLISHING,

        STATUS_CONNECTION_ESTABLISHED,
    }
}