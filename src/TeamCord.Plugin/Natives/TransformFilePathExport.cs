using System.Runtime.InteropServices;

namespace TeamCord.Plugin.Natives
{
    public struct TransformFilePathExport
    {
        private ulong channel;

        [MarshalAs(UnmanagedType.LPWStr)]
        private string filename;

        private int action;
        private int transformedFileNameMaxSize;
        private int channelPathMaxSize;
    }
}