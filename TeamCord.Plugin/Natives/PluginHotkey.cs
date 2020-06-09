using System.Runtime.InteropServices;

namespace TeamCord.Plugin.Natives
{
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct PluginHotkey
    {
        /// char[128]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string keyword;

        /// char[128]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string description;
    }
}