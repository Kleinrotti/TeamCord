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
        public static Version CurrentConfigVersion { get; } = new Version(1, 1, 0, 1);

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

        [ControlType(typeof(CheckBox))]
        [DisplayName("Show discord id")]
        [Description("When enabled, TeamCord creates a client description for your teamspeak user with your discord id.\n" +
            " This allows automuting when other users using TeamCord too and are also connected to that same channel to avoid doubled audio. \nNOTE: Other teamspeak users can find your discord profile this way too!")]
        public bool EnableDiscordID { get; set; }
    }
}