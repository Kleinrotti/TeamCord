using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace TeamCord.Core
{
    public class PluginSettings
    {
        /// <summary>
        /// Current version of the config file
        /// </summary>
        public static Version CurrentConfigVersion { get; } = new Version(1, 1, 0, 0);

        public Version ConfigVersion { get; set; }

        [ControlType(typeof(CheckBox))]
        [DisplayName("Use Teamspeaks voice activation")]
        public bool UseTeamspeakVoiceActivation { get; set; } = true;

        [ControlType(typeof(CheckBox))]
        [DisplayName("Join discord channels automatically")]
        [Description("When connecting to a Teamspeak channel, TeamCord will join that linked Discord channel too.")]
        public bool AutomaticChannelJoin { get; set; }

        [ControlType(typeof(CheckBox))]
        [DisplayName("Auto login to discord")]
        [Description("When connecting to a Teamspeak server, TeamCord will log you in to Discord")]
        public bool DiscordAutoLogin { get; set; } = true;

        [ControlType(typeof(CheckBox))]
        [DisplayName("")]
        public bool ShowConnectionStatus { get; set; }

        [DisplayName("Token")]
        public PluginUserCredential Token { get; set; }

        [ControlType(typeof(CheckBox))]
        [DisplayName("Debug log")]
        public bool DebugLogging { get; set; }

        [ControlType(typeof(CheckBox))]
        [DisplayName("Notifications")]
        public bool Notifications { get; set; } = true;
    }
}