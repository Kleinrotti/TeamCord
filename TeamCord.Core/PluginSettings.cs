namespace TeamCord.Core
{
    public class PluginSettings
    {
        public bool UseTeamspeakRawAudio { get; set; } = true;
        public bool AutomaticJoin { get; set; } = true;
        public bool ShowConnectionStatus { get; set; } = false;
        public PluginUserCredentials PluginUserCredentials { get; set; }
    }
}