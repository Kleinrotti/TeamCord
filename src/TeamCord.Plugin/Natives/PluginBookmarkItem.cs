using System;
using System.Runtime.InteropServices;

namespace TeamCord.Plugin.Natives
{
    [StructLayoutAttribute(LayoutKind.Explicit)]
    public struct Anonymous_344b905a_b983_4217_a33a_e37f6170018f
    {
        /// char*
        [FieldOffsetAttribute(0)]
        public IntPtr uuid;

        /// PluginBookmarkList*
        [FieldOffsetAttribute(0)]
        public IntPtr folder;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct PluginBookmarkItem
    {
        /// char*
        [MarshalAs(UnmanagedType.LPStr)]
        public string name;

        /// unsigned char
        public byte isFolder;

        /// unsigned char[3]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string reserved;

        /// Anonymous_344b905a_b983_4217_a33a_e37f6170018f
        public Anonymous_344b905a_b983_4217_a33a_e37f6170018f Union1;
    }
}