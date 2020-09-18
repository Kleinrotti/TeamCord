using Newtonsoft.Json;

namespace TeamCord.Plugin
{
    internal class TS3Json<T>
    {
        public TS3Json(T value)
        {
            Teamcord = value;
        }

        public T Teamcord
        {
            get; set;
        }
    }

    internal class TS3ChannelJson
    {
        [JsonProperty("Channel")]
        public virtual ulong ChannelID { get; set; }

        public TS3ChannelJson(ulong channelID)
        {
            ChannelID = channelID;
        }
    }

    internal class TS3ClientJson
    {
        [JsonProperty("Client")]
        public virtual ulong ClientID { get; set; }

        public TS3ClientJson(ulong clientID)
        {
            ClientID = clientID;
        }
    }
}