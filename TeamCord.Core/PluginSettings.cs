namespace TeamCord.Core
{
    public class PluginSettings
    {
        public bool UseTeamspeakVoiceActivation { get; set; } = true;
        public bool AutomaticChannelJoin { get; set; } = true;
        public bool DiscordAutoLogin { get; set; } = true;
        public bool ShowConnectionStatus { get; set; }
        public PluginUserCredential Email { get; set; }
        public PluginUserCredential Password { get; set; }
        public bool DebugLogging { get; set; }
        public bool Notifications { get; set; } = true;
    }
}