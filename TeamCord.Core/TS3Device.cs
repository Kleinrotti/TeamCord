using System;

namespace TeamCord.Core
{
    public class TS3Device
    {
        public TS3Device(TS3DeviceType deviceType, string deviceId, string deviceName)
        {
            DeviceType = deviceType;
            DeviceID = deviceId;
            DeviceName = deviceName;
        }

        public TS3Device(IntPtr devicePointer)
        {
        }

        public TS3DeviceType DeviceType { get; }
        public string DeviceID { get; }
        public string DeviceName { get; }
        public bool IsDefault { get; set; }
    }
}