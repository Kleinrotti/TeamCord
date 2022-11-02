using Forge.Forms.Annotations;
using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace TeamCord.Core
{
    [Title("Settings")]
    [Action("apply", "Apply", IsDefault = true, ClosesDialog = true, Validates = true)]
    public class SettingsModel : IFileStorable<SettingsModel>
    {
        [Field(IsVisible = false)]
        public PluginUserCredential Token { get; set; }

        [Field(Name = "Teamspeak voice activation",
            ToolTip = "Uses teamspeaks implementation of push to talk or voice activation instead of continuous transmission.")]
        public bool UseTeamspeakVoiceActivation { get; set; } = true;

        [Field(Name = "Discord auto channel join",
            ToolTip = "When connecting to a Teamspeak channel, TeamCord will join that linked Discord channel too.")]
        public bool AutomaticChannelJoin { get; set; }

        [Field(Name = "Auto login",
            ToolTip = "When connecting to a Teamspeak server, TeamCord will log you in to Discord.")]
        public bool DiscordAutoLogin { get; set; }

        [Field(Name = "Debugmode", ToolTip = "Enable debug logging.")]
        public bool DebugLogging { get; set; }

        [Field(Name = "Notifications", ToolTip = "Enable desktop notifications.")]
        public bool Notifications { get; set; } = true;

        [Field(Name = "Discord user detection",
            ToolTip = "When enabled, TeamCord creates a client description for your teamspeak user with your discord id.\n" +
            " This allows automuting when other users using TeamCord too and are also connected to that same channel to avoid doubled audio. " +
            "\nNOTE: Other teamspeak users can find your discord profile this way too!")]
        public bool EnableDiscordID { get; set; }

        [Field(Name = "Check for updates", ToolTip = "Check for updates when Teamspeak starts.")]
        [Description("Automatically check for Teamcord updates when Teamspeak starts.")]
        public bool AutoUpdateCheck { get; set; } = true;

        public SettingsModel Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<SettingsModel>(json);
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}