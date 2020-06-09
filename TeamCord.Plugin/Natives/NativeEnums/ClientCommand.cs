namespace TeamCord.Plugin.Natives
{
    public enum ClientCommand
    {
        CLIENT_COMMAND_requestConnectionInfo = 0,
        CLIENT_COMMAND_requestClientMove = 1,
        CLIENT_COMMAND_requestXXMuteClients = 2,
        CLIENT_COMMAND_requestClientKickFromXXX = 3,
        CLIENT_COMMAND_flushChannelCreation = 4,
        CLIENT_COMMAND_flushChannelUpdates = 5,
        CLIENT_COMMAND_requestChannelMove = 6,
        CLIENT_COMMAND_requestChannelDelete = 7,
        CLIENT_COMMAND_requestChannelDescription = 8,
        CLIENT_COMMAND_requestChannelXXSubscribeXXX = 9,
        CLIENT_COMMAND_requestServerConnectionInfo = 10,
        CLIENT_COMMAND_requestSendXXXTextMsg = 11,
        CLIENT_COMMAND_filetransfers = 12,
        CLIENT_COMMAND_ENDMARKER
    }
}