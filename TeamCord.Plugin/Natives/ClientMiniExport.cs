using System.Runtime.InteropServices;

namespace TeamCord.Plugin.Natives
{
    public struct ClientMiniExport
    {
        private short ID;
        private ulong channel;

        [MarshalAs(UnmanagedType.LPWStr)]
        private string ident;

        [MarshalAs(UnmanagedType.LPWStr)]
        private string nickname;
    }
}