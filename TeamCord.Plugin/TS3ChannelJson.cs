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
        public string ChannelID { get; set; }
    }
}