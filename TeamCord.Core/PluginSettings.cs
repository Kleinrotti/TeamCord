namespace TeamCord.Core
{
    public class PluginSettings
    {
        public bool UseTeamspeakVoiceActivation { get; set; } = true;
        public bool AutomaticChannelJoin { get; set; } = true;
        public bool DiscordAutoLogin { get; set; } = true;
        public bool ShowConnectionStatus { get; set; } = false;
        public PluginUserCredentials PluginUserCredentials { get; set; }
        public bool DebugLogging { get; set; }
    }
}