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
                var obj = JsonConvert.DeserializeObject<TS3ChannelJson>(channelDescription);

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
            var obj = new TS3ChannelJson();
            var val = new Teamcord();
            val.ChannelID = channelID;
            obj.Teamcord = val;
            var json = JsonConvert.SerializeObject(obj);
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