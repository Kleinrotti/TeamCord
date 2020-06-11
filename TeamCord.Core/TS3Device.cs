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

        public TS3Device(TS3DeviceType deviceType, string deviceId, string deviceName, bool isDefault)
        {
            DeviceType = deviceType;
            DeviceID = deviceId;
            DeviceName = deviceName;
            IsDefault = isDefault;
        }

        public TS3Device(TS3DeviceType deviceType, string[] deviceArray)
        {
            DeviceType = deviceType;
            DeviceID = deviceArray[1];
            DeviceName = deviceArray[0];
        }

        public TS3Device(TS3DeviceType deviceType, string[] deviceArray, bool isDefault)
        {
            DeviceType = deviceType;
            DeviceID = deviceArray[1];
            DeviceName = deviceArray[0];
            IsDefault = isDefault;
        }

        public TS3DeviceType DeviceType { get; }
        public string DeviceID { get; }
        public string DeviceName { get; }
        public bool IsDefault { get; set; }
    }
}