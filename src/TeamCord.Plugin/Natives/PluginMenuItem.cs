using System.Runtime.InteropServices;

namespace TeamCord.Plugin.Natives
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public unsafe struct PluginMenuItem
    {
        /// PluginMenuType
        public PluginMenuType type;

        /// int
        public int id;

        public fixed char text[128];

        public fixed char icon[128];
        /// char[128]
        // [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        // public string text;

        /// char[128]
        //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        //public string icon;
    }
}