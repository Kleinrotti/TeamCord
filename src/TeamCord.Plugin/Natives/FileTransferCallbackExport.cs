using System.Runtime.InteropServices;

namespace TeamCord.Plugin.Natives
{
    public struct FileTransferCallbackExport
    {
        private short clientID;
        private short transferID;
        private short remoteTransferID;
        private int status;

        [MarshalAs(UnmanagedType.LPWStr)]
        private string statusMessage;

        private ulong remotefileSize;
        private ulong bytes;
        private int isSender;
    }
}