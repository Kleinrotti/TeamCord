using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
