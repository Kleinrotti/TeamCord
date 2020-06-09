using System.Runtime.InteropServices;

namespace TeamCord.Plugin.Natives
{
    public struct VariablesExportItem
    {
        public char itemIsValid;    /* This item has valid values. ignore this item if 0 */
        public char proposedIsSet;  /* The value in proposed is set. if 0 ignore proposed */

        [MarshalAs(UnmanagedType.LPWStr)]
        public string current;          /* current value (stored in memory) */

        [MarshalAs(UnmanagedType.LPWStr)]
        public string proposed;         /* New value to change to (const, so no updates please) */
    };
}