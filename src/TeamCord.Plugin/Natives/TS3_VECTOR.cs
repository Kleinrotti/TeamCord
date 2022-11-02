using System.Runtime.InteropServices;

namespace TeamCord.Plugin.Natives
{
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct TS3_VECTOR
    {
        /// float
        public float x;

        /// float
        public float y;

        /// float
        public float z;
    }
}