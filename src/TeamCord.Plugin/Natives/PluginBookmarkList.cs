using System.Runtime.InteropServices;

namespace TeamCord.Plugin.Natives
{
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct PluginBookmarkList
    {
        /// int
        public int itemcount;

        /// PluginBookmarkItem[1]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.Struct)]
        public PluginBookmarkItem[] items;
    }
}