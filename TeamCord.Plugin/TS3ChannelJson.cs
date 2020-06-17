using Newtonsoft.Json;

namespace TeamCord.Plugin
{
    internal class TS3ChannelJson
    {
        public Teamcord Teamcord { get; set; }
    }

    internal struct Teamcord
    {
        [JsonProperty("Channel")]
        public ulong ChannelID { get; set; }
    }
}