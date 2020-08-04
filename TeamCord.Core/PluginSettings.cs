using System.ComponentModel;
using System.Windows.Controls;

namespace TeamCord.Core
{
    public class PluginSettings
    {
        [ControlType(typeof(CheckBox))]
        [DisplayName("Use Teamspeaks voice activation")]
        public bool UseTeamspeakVoiceActivation { get; set; } = true;

        [ControlType(typeof(CheckBox))]
        [DisplayName("Join discord channels automatically")]
        public bool AutomaticChannelJoin { get; set; } = true;

        [ControlType(typeof(CheckBox))]
        [DisplayName("Auto login to discord")]
        public bool DiscordAutoLogin { get; set; } = true;

        [ControlType(typeof(CheckBox))]
        [DisplayName("")]
        public bool ShowConnectionStatus { get; set; }

        [ControlType(typeof(TextBox))]
        [DisplayName("Email")]
        public PluginUserCredential Email { get; set; }

        [ControlType(typeof(PasswordBox))]
        [DisplayName("Password")]
        public PluginUserCredential Password { get; set; }

        [ControlType(typeof(CheckBox))]
        [DisplayName("Debug log")]
        public bool DebugLogging { get; set; }

        [ControlType(typeof(CheckBox))]
        [DisplayName("Notifications")]
        public bool Notifications { get; set; } = true;
    }
}