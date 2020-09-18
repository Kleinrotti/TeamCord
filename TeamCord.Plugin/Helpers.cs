using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TeamCord.Core;

namespace TeamCord.Plugin
{
    public static class Helpers
    {
        public static ulong ExtractChannelID(string channelDescription)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject<TS3Json<TS3ChannelJson>>(channelDescription);

                return obj.Teamcord.ChannelID;
            }
            catch (NullReferenceException ex)
            {
                Logging.Log(ex.Message, LogLevel.LogLevel_DEBUG);
                return 0;
            }
            catch (Exception ex)
            {
                Logging.Log(ex.Message, LogLevel.LogLevel_WARNING);
                return 0;
            }
        }

        public static string ChannelIDToJsonString(ulong channelID)
        {
            var val = new TS3Json<TS3ChannelJson>(new TS3ChannelJson(channelID));
            var json = JsonConvert.SerializeObject(val);
            return json;
        }

        public static string DiscordIDToJsonString(ulong discordID)
        {
            var val = new TS3Json<TS3ClientJson>(new TS3ClientJson(discordID));
            var json = JsonConvert.SerializeObject(val);
            return json;
        }

        public static string UserListToTs3String(IList<string> userList)
        {
            string data = "[b][color=red]--- Userlist ---[/color][/b]\n";
            foreach (var v in userList)
            {
                data += "- " + v + "\n";
            }
            return data;
        }
    }
}