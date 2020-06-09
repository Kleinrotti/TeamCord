using System.Runtime.InteropServices;

namespace TeamCord.Plugin.Natives
{
    public struct TransformFilePathExportReturns
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        private string transformedFileName;

        [MarshalAs(UnmanagedType.LPWStr)]
        private string channelPath;

        private int logFileAction;
    }
}